namespace ChatApp.ViewModels
{
    public class LoginSuccessMessage
    {
        public bool IsAdmin { get; set; }
        public string UserName { get; set; }

        public LoginSuccessMessage(string userName, bool isAdmin)
        {
            UserName = userName;
            IsAdmin = isAdmin;
        }
    }
}