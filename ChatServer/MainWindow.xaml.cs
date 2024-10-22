using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ChatServer
{
    public partial class MainWindow : Window
    {
        private ServerSocket _server;
        private static Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            _server = new ServerSocket();
            _server.OnMessageLogged += LogMessageToUI;
            _server.OnClientListUpdated += UpdateClientListUI;

            // Set default IP
            IpTextBox.Text = "127.0.0.1";
            CancelServerButton.IsEnabled = false; // Disable Cancel button at startup
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
                await _server.StartServer(ipAddress, port);
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
            _server.StopServer();
            StartServerButton.IsEnabled = true; // Enable Start button when server stops
            CancelServerButton.IsEnabled = false; // Disable Cancel button when server stops
        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            // Only randomize the port
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

            await _server.SendMessageToClient(selectedClient, message);
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

            await _server.SendMessageToAll(message);
            MessageTextBox.Clear();
        }
    }
}
