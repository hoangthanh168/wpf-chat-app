using ChatApp.Core.Models;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp
{
    public class ClientSocket
    {
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _listeningTask;

        public bool IsConnected
        {
            get
            {
                return _tcpClient != null && _tcpClient.Connected;
            }
        }

        public event Action OnConnected;
        public event Action OnDisconnected;
        public event Action<ChatDTO.ServerMessageDTO> OnMessageReceived;

        public ClientSocket()
        {
            _tcpClient = new TcpClient();
        }

        public async Task ConnectAsync(string ipAddress, int port)
        {
            if (IsConnected)
            {
                throw new InvalidOperationException("Already connected to the server.");
            }

            try
            {
                await _tcpClient.ConnectAsync(ipAddress, port);
                _networkStream = _tcpClient.GetStream();
                OnConnected?.Invoke();

                _cancellationTokenSource = new CancellationTokenSource();
                _listeningTask = Task.Run(() => ListenForMessagesAsync(_cancellationTokenSource.Token));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task ListenForMessagesAsync(CancellationToken cancellationToken)
        {
            var buffer = new byte[4096];

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    int bytesRead = await _networkStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);

                    if (bytesRead == 0)
                    {
                        break;
                    }

                    string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    try
                    {
                        var serverMessage = JsonConvert.DeserializeObject<ChatDTO.ServerMessageDTO>(receivedData);
                        OnMessageReceived?.Invoke(serverMessage);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception)
            {
            }
            finally
            {
                Disconnect();
            }
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                _cancellationTokenSource?.Cancel();

                _networkStream?.Close();
                _tcpClient.Close();
                _tcpClient = new TcpClient();

                OnDisconnected?.Invoke();
            }
        }

        public async Task SendMessageAsync(ChatDTO.ClientMessageDTO clientMessage)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Not connected to the server.");
            }

            try
            {
                string messageJson = JsonConvert.SerializeObject(clientMessage);
                byte[] data = Encoding.UTF8.GetBytes(messageJson);

                await _networkStream.WriteAsync(data, 0, data.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
