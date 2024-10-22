using ChatApp.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ChatApp.Views
{
    public partial class LoginPage : UserControl
    {
        private readonly LoginViewModel _viewModel;

        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }
        public LoginPage()
        {
                
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
