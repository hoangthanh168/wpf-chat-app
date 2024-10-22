using ChatApp.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ChatApp.Views
{
    public partial class LoginView : UserControl
    {
        private readonly LoginViewModel _viewModel;

        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
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
