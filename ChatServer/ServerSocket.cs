using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Core.Models;
using ChatApp.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChatServer
{
    public class ServerSocket
    {
        private TcpListener _listener;
        private bool _isRunning;
        private readonly ConcurrentDictionary<string, TcpClient> _clients =
            new ConcurrentDictionary<string, TcpClient>();
        private readonly IServiceScopeFactory _scopeFactory;

        public event Action<string> OnMessageLogged;
        public event Action<List<string>> OnClientListUpdated;

        public ServerSocket(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartServer(string ipAddress, int port)
        {
            try
            {
                IPAddress ip;
                if (ipAddress == "localhost" || ipAddress == "127.0.0.1")
                {
                    ip = IPAddress.Loopback;
                }
                else if (ipAddress == "0.0.0.0")
                {
                    ip = IPAddress.Any;
                }
                else
                {
                    ip = IPAddress.Parse(ipAddress);
                }

                _listener = new TcpListener(ip, port);
                _listener.Start();
                _isRunning = true;

                LogMessage($"Máy chủ đã khởi động tại {ip}:{port}");

                while (_isRunning)
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync();
                    HandleClient(client);
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Lỗi: {ex.Message}");
            }
        }

        public void StopServer()
        {
            if (_isRunning)
            {
                _isRunning = false;
                _listener.Stop();
                LogMessage("Máy chủ đã dừng.");

                foreach (var client in _clients.Values)
                {
                    client.Close();
                }
                _clients.Clear();
                using (var scope = _scopeFactory.CreateScope())
                {
                    var sessionService =
                        scope.ServiceProvider.GetRequiredService<UserSessionService>();
                    sessionService.CleanUpInactiveSessions(TimeSpan.Zero);
                }
                UpdateClientList();
            }
        }

        private async void HandleClient(TcpClient client)
        {
            string clientEndpoint = client.Client.RemoteEndPoint.ToString();
            if (_clients.TryAdd(clientEndpoint, client))
            {
                LogMessage($"Client đã kết nối: {clientEndpoint}");
                UpdateClientList();

                try
                {
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        var dto = JsonConvert.DeserializeObject<ChatDTO.ClientMessageDTO>(
                            receivedData
                        );

                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var userService =
                                scope.ServiceProvider.GetRequiredService<UserService>();
                            var messageService =
                                scope.ServiceProvider.GetRequiredService<MessageService>();
                            var sessionService =
                                scope.ServiceProvider.GetRequiredService<UserSessionService>();

                            switch (dto.Type)
                            {
                                case ChatDTO.MessageType.Login:
                                    await HandleLogin(
                                        dto,
                                        clientEndpoint,
                                        userService,
                                        sessionService
                                    );
                                    break;
                                case ChatDTO.MessageType.SendMessage:
                                    await HandleSendMessage(
                                        dto,
                                        clientEndpoint,
                                        messageService,
                                        sessionService
                                    );
                                    break;
                                default:
                                    LogMessage($"Kiểu tin nhắn không xác định từ {clientEndpoint}");
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogMessage($"Lỗi từ Client {clientEndpoint}: {ex.Message}");
                }
                finally
                {
                    if (_clients.TryRemove(clientEndpoint, out _))
                    {
                        client.Close();
                        LogMessage($"Client đã ngắt kết nối: {clientEndpoint}");
                        UpdateClientList();

                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var sessionService =
                                scope.ServiceProvider.GetRequiredService<UserSessionService>();
                            sessionService.CleanUpInactiveSessions(TimeSpan.Zero);
                        }
                    }
                }
            }
            else
            {
                LogMessage($"Không thể thêm Client: {clientEndpoint}");
                client.Close();
            }
        }

        private async Task HandleSendMessage(
            ChatDTO.ClientMessageDTO dto,
            string clientEndpoint,
            MessageService messageService,
            UserSessionService sessionService
        )
        {
            var sendMessageRequest = ((JObject)dto.Payload).ToObject<ChatDTO.SendMessageDTO>();

            var message = new Message
            {
                SenderID = sendMessageRequest.SenderId,
                Content = sendMessageRequest.Content,
                SentAt = DateTime.UtcNow,
            };

            if (sendMessageRequest.IsGroupMessage)
            {
                message.GroupID = sendMessageRequest.GroupId;
                messageService.SaveMessage(message);

                await SendMessageToAll(
                    $"{sendMessageRequest.SenderId}: {sendMessageRequest.Content}"
                );
            }
            else
            {
                message.ReceiverID = sendMessageRequest.ReceiverId;
                messageService.SaveMessage(message);

                var receiverSession = sessionService.GetActiveSession(
                    (int)sendMessageRequest.ReceiverId
                );
                if (receiverSession != null)
                {
                    await SendMessageToClient(
                        receiverSession.ClientEndpoint,
                        $"{sendMessageRequest.SenderId}: {sendMessageRequest.Content}"
                    );
                }
            }
        }

        private async Task HandleLogin(
            ChatDTO.ClientMessageDTO dto,
            string clientEndpoint,
            UserService userService,
            UserSessionService sessionService
        )
        {
            var loginRequest = ((JObject)dto.Payload).ToObject<ChatDTO.LoginRequestDTO>();
            var user = userService.Authenticate(loginRequest.Username, loginRequest.PasswordHash);

            if (user != null)
            {
                sessionService.CreateSession(user.UserID, clientEndpoint);
            }

            string message =
                user != null ? "Đăng nhập thành công." : "Thông tin đăng nhập không hợp lệ.";
            await SendMessageToClient(clientEndpoint, message);
        }

        public async Task SendMessageToClient(string clientEndpoint, string message)
        {
            if (_clients.TryGetValue(clientEndpoint, out TcpClient client))
            {
                try
                {
                    var sendMessageDTO = new ChatDTO.SendMessageDTO
                    {
                        SenderId = 0,
                        Content = message,
                        IsGroupMessage = false,
                        SentAt = DateTime.UtcNow,
                    };

                    NetworkStream stream = client.GetStream();

                    var serverMessage = new ChatDTO.ServerMessageDTO
                    {
                        Type = ChatDTO.MessageType.SendMessage,
                        Payload = sendMessageDTO,
                    };

                    string messageJson = JsonConvert.SerializeObject(serverMessage);

                    byte[] data = Encoding.UTF8.GetBytes(messageJson);

                    await stream.WriteAsync(data, 0, data.Length);

                    LogMessage($"Gửi tới {clientEndpoint}: {message}");
                }
                catch (Exception ex)
                {
                    LogMessage($"Lỗi khi gửi tới {clientEndpoint}: {ex.Message}");
                }
            }
            else
            {
                LogMessage($"Không tìm thấy Client {clientEndpoint}.");
            }
        }

        private void LogMessage(string message) => OnMessageLogged?.Invoke(message);

        private void UpdateClientList()
        {
            var clientList = new List<string>(_clients.Keys);
            OnClientListUpdated?.Invoke(clientList);
        }

        public async Task SendMessageToAll(string message)
        {
            foreach (var clientEndpoint in _clients.Keys)
            {
                await SendMessageToClient(clientEndpoint, message);
            }
            LogMessage($"Gửi tới tất cả: {message}");
        }
    }
}
