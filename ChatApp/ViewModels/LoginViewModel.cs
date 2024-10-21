using ChatApp.Mvvm;

namespace ChatApp.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public DelegateCommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private void ExecuteLogin()
        {
            // Perform login logic here
            bool isLoginSuccessful = true; // Replace with actual login check
            bool isAdmin = false; // Replace with actual admin check

            if (isLoginSuccessful)
            {
                // Use our custom Messenger to send the login success message
                Messenger.Send(new LoginSuccessMessage(Username, isAdmin));
            }
        }
    }
}