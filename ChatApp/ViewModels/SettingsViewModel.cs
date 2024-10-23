using ChatApp.Mvvm;
using System;
using System.Windows.Input;
using System.Threading.Tasks;
using ChatApp.Core.Models;
using Newtonsoft.Json.Linq;

namespace ChatApp.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        private string _ipAddress;
        private string _port;
        private string _connectionStatus;
        private ClientSocket _clientSocket;
        private readonly ShellViewModel _shellViewModel;
        private readonly UserSession _userSession;

        public SettingsViewModel(ClientSocket clientSocket, ShellViewModel shellViewModel, UserSession userSession)
        {
            _clientSocket = clientSocket;
            _shellViewModel = shellViewModel;
            _userSession = userSession;
            ConnectCommand = new DelegateCommand(async () => await ConnectToServer());
            DisconnectCommand = new DelegateCommand(DisconnectFromServer);
            LogoutCommand = new DelegateCommand(Logout);

            _clientSocket.OnConnected += HandleConnected;
            _clientSocket.OnDisconnected += HandleDisconnected;

            ConnectionStatus = "Chưa kết nối";
        }

        public string IpAddress
        {
            get => _ipAddress;
            set => SetProperty(ref _ipAddress, value);
        }

        public string Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }

        public string ConnectionStatus
        {
            get => _connectionStatus;
            set => SetProperty(ref _connectionStatus, value);
        }
        
        public string Username => _userSession.CurrentUser?.Username;

        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }
        public ICommand LogoutCommand { get; }

        private async Task ConnectToServer()
        {
            if (string.IsNullOrEmpty(IpAddress) || string.IsNullOrEmpty(Port))
            {
                ConnectionStatus = "Vui lòng cung cấp cả địa chỉ IP và cổng.";
                return;
            }

            if (!int.TryParse(Port, out int portNumber))
            {
                ConnectionStatus = "Số cổng không hợp lệ.";
                return;
            }

            try
            {
                await _clientSocket.ConnectAsync(IpAddress, portNumber);

                var loginDto = new ChatDTO.ClientMessageDTO
                {
                    Type = ChatDTO.MessageType.Login,

                    Payload = JObject.FromObject(new ChatDTO.LoginRequestDTO
                    {
                        Username = Username,
                        PasswordHash = _userSession.CurrentUser.PasswordHash
                    })
                };
                await _clientSocket.SendMessageAsync(loginDto);

            }
            catch (Exception ex)
            {
                ConnectionStatus = $"Kết nối thất bại: {ex.Message}";
            }
        }

        private void DisconnectFromServer()
        {
            if (_clientSocket.IsConnected)
            {
                _clientSocket.Disconnect();
                ConnectionStatus = "Ngắt kết nối thành công!";
            }
            else
            {
                ConnectionStatus = "Đã ngắt kết nối.";
            }
        }

        private void Logout()
        {
            if (_clientSocket.IsConnected)
            {
                _clientSocket.Disconnect();
            }

            ConnectionStatus = "Đã ngắt kết nối";

            _shellViewModel.ShowLoginView();
        }

        private void HandleConnected()
        {
            ConnectionStatus = "Kết nối thành công!";
        }

        private void HandleDisconnected()
        {
            ConnectionStatus = "Đã ngắt kết nối.";
        }
    }
}
