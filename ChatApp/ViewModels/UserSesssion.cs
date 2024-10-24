using ChatApp.Core.Models;

namespace ChatApp.ViewModels
{
    public class UserSession
    {
        public User CurrentUser { get; set; }

        public bool IsLoggedIn => CurrentUser != null;
    }
}
