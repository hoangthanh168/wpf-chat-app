using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class ServerSocket
    {
        private TcpListener _listener;
        private bool _isRunning;
        private readonly ConcurrentDictionary<string, TcpClient> _clients = new ConcurrentDictionary<string, TcpClient>();

        public event Action<string> OnMessageLogged;
        public event Action<List<string>> OnClientListUpdated;

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

                LogMessage($"Server started on {ip}:{port}");

                while (_isRunning)
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync();
                    HandleClient(client);
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Error: {ex.Message}");
            }
        }

        public void StopServer()
        {
            if (_isRunning)
            {
                _isRunning = false;
                _listener.Stop();
                LogMessage("Server stopped.");

                // Disconnect all clients
                foreach (var client in _clients.Values)
                {
                    client.Close();
                }
                _clients.Clear();
                UpdateClientList();
            }
        }

        private async void HandleClient(TcpClient client)
        {
            string clientEndpoint = client.Client.RemoteEndPoint.ToString();
            if (_clients.TryAdd(clientEndpoint, client))
            {
                LogMessage($"Client connected: {clientEndpoint}");
                UpdateClientList();

                try
                {
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        LogMessage($"Received from {clientEndpoint}: {message}");

                        // Optionally, you can broadcast the message to other clients
                        // await BroadcastMessage($"{clientEndpoint}: {message}", clientEndpoint);
                    }
                }
                catch (Exception ex)
                {
                    LogMessage($"Client {clientEndpoint} error: {ex.Message}");
                }
                finally
                {
                    if (_clients.TryRemove(clientEndpoint, out _))
                    {
                        client.Close();
                        LogMessage($"Client disconnected: {clientEndpoint}");
                        UpdateClientList();
                    }
                }
            }
            else
            {
                LogMessage($"Failed to add client: {clientEndpoint}");
                client.Close();
            }
        }

        private void LogMessage(string message)
        {
            OnMessageLogged?.Invoke(message);
        }

        private void UpdateClientList()
        {
            var clientList = new List<string>(_clients.Keys);
            OnClientListUpdated?.Invoke(clientList);
        }

        // Method to send a message to a specific client
        public async Task SendMessageToClient(string clientEndpoint, string message)
        {
            if (_clients.TryGetValue(clientEndpoint, out TcpClient client))
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    await stream.WriteAsync(data, 0, data.Length);
                    LogMessage($"Sent to {clientEndpoint}: {message}");
                }
                catch (Exception ex)
                {
                    LogMessage($"Error sending to {clientEndpoint}: {ex.Message}");
                }
            }
            else
            {
                LogMessage($"Client {clientEndpoint} not found.");
            }
        }

        // Method to send a message to all connected clients
        public async Task SendMessageToAll(string message)
        {
            foreach (var clientEndpoint in _clients.Keys)
            {
                await SendMessageToClient(clientEndpoint, message);
            }
            LogMessage($"Sent to all: {message}");
        }

        // Optional: Broadcast message to all clients except the sender
        public async Task BroadcastMessage(string message, string senderEndpoint)
        {
            foreach (var clientEndpoint in _clients.Keys)
            {
                if (clientEndpoint != senderEndpoint)
                {
                    await SendMessageToClient(clientEndpoint, message);
                }
            }
            LogMessage($"Broadcasted: {message}");
        }
    }
}
