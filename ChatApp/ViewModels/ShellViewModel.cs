using System;
using System.Collections.ObjectModel;
using ChatApp.Mvvm;
using ChatApp.Views;

namespace ChatApp.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public ObservableCollection<MenuItem> Menu { get; }
        public ObservableCollection<MenuItem> OptionsMenu { get; }

        public ShellViewModel()
        {
            Menu = new ObservableCollection<MenuItem>
            {
                new MenuItem { Label = "Chat", Icon = "\uE8BD", NavigationDestination = new Uri("Views/ChatPage.xaml", UriKind.RelativeOrAbsolute), IsNavigation = true }
            };
            OptionsMenu = new ObservableCollection<MenuItem>
            {
                new MenuItem { Label = "Settings", Icon = "\uE713", NavigationDestination = new Uri("Views/SettingsPage.xaml", UriKind.RelativeOrAbsolute), IsNavigation = true },
            };
        }
    }
}