using ChatApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ViewModels
{
    public class UserSession
    {
        public User CurrentUser { get; set; }

        public bool IsLoggedIn => CurrentUser != null;
    }
}
