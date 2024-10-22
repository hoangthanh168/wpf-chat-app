using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public class ClientSocket
    {
        private TcpClient _tcpClient;

        public ClientSocket()
        {
            _tcpClient = new TcpClient();
        }

        public bool IsConnected
        {
            get
            {
                return _tcpClient.Connected && _tcpClient.Client != null && _tcpClient.Client.Connected;
            }
        }

        public void Connect(string ipAddress, int port)
        {
            if (IsConnected)
            {
                throw new InvalidOperationException("Already connected to the server.");
            }

            _tcpClient.Connect(ipAddress, port);
        }

        public async Task SendMessageAsync(string message)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Not connected to the server.");
            }

            NetworkStream stream = _tcpClient.GetStream();
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                _tcpClient.Close();
                _tcpClient = new TcpClient();
            }
        }
    }
}
