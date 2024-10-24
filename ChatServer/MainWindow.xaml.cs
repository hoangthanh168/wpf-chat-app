using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace ChatServer
{
    public partial class MainWindow : Window
    {
        private readonly ServerSocket _serverSocket;
        private static Random _random = new Random();

        public MainWindow(ServerSocket serverSocket)
        {
            InitializeComponent();
            _serverSocket = serverSocket;
            _serverSocket.OnMessageLogged += LogMessageToUI;
            _serverSocket.OnClientListUpdated += UpdateClientListUI;

            IpTextBox.Text = "127.0.0.1";
            CancelServerButton.IsEnabled = false;
        }

        private async void StartServer_Click(object sender, RoutedEventArgs e)
        {
            string ipAddress = IpTextBox.Text;
            if (!int.TryParse(PortTextBox.Text, out int port))
            {
                LogMessageToUI("Invalid port number");
                return;
            }

            StartServerButton.IsEnabled = false;
            CancelServerButton.IsEnabled = true;

            try
            {
                await _serverSocket.StartServer(ipAddress, port);
            }
            catch (Exception ex)
            {
                LogMessageToUI($"Failed to start server: {ex.Message}");
                StartServerButton.IsEnabled = true;
                CancelServerButton.IsEnabled = false;
            }
        }

        private void CancelServer_Click(object sender, RoutedEventArgs e)
        {
            _serverSocket.StopServer();
            StartServerButton.IsEnabled = true;
            CancelServerButton.IsEnabled = false;
        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            int randomPort = _random.Next(1024, 65535);
            PortTextBox.Text = randomPort.ToString();
        }

        private void LogMessageToUI(string message)
        {
            Dispatcher.Invoke(() =>
            {
                LogTextBox.AppendText($"{DateTime.Now}: {message}\n");
                LogTextBox.ScrollToEnd();
            });
        }

        private void UpdateClientListUI(List<string> clients)
        {
            Dispatcher.Invoke(() =>
            {
                ClientsListBox.ItemsSource = null;
                ClientsListBox.ItemsSource = clients;
            });
        }

        private async void SendToSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedClient = ClientsListBox.SelectedItem as string;
            string message = MessageTextBox.Text.Trim();

            if (string.IsNullOrEmpty(selectedClient))
            {
                LogMessageToUI("No client selected.");
                return;
            }

            if (string.IsNullOrEmpty(message))
            {
                LogMessageToUI("Message is empty.");
                return;
            }

            await _serverSocket.SendMessageToClient(selectedClient, message, 1);
            MessageTextBox.Clear();
        }

        private async void SendToAllButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageTextBox.Text.Trim();

            if (string.IsNullOrEmpty(message))
            {
                LogMessageToUI("Message is empty.");
                return;
            }

            await _serverSocket.SendMessageToAll(message);
            MessageTextBox.Clear();
        }
    }
}
