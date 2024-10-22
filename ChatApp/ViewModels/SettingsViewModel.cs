using ChatApp.Mvvm;
using System;
using System.Windows.Input;

namespace ChatApp.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        private string _ipAddress;
        private string _port;
        private string _connectionStatus;
        private ClientSocket _clientSocket;
        private readonly ShellViewModel _shellViewModel;

        public SettingsViewModel(ClientSocket clientSocket, ShellViewModel shellViewModel)
        {
            _clientSocket = clientSocket;
            _shellViewModel = shellViewModel;
            ConnectCommand = new DelegateCommand(ConnectToServer);
            LogoutCommand = new DelegateCommand(Logout);
            ConnectionStatus = "Disconnected";
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

        public ICommand ConnectCommand { get; }
        public ICommand LogoutCommand { get; }

        private void ConnectToServer()
        {
            if (string.IsNullOrEmpty(IpAddress) || string.IsNullOrEmpty(Port))
            {
                ConnectionStatus = "Please provide both IP address and port.";
                return;
            }

            if (!int.TryParse(Port, out int portNumber))
            {
                ConnectionStatus = "Invalid port number.";
                return;
            }

            try
            {
                _clientSocket.Connect(IpAddress, portNumber);
                ConnectionStatus = "Connected successfully!";
            }
            catch (Exception ex)
            {
                ConnectionStatus = $"Connection failed: {ex.Message}";
            }
        }

        private void Logout()
        {
            if (_clientSocket.IsConnected)
            {
                _clientSocket.Disconnect();
            }

            ConnectionStatus = "Disconnected";

            // Navigate back to the login view
            _shellViewModel.ShowLoginView();
        }
    }
}
