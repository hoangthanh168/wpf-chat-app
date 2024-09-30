using System;
using System.Collections.ObjectModel;
using ChatApp.Mvvm;
using System.Configuration;
using MahApps.Metro.IconPacks;
using ChatApp.Mvvm;
using ChatApp.Views;

namespace ChatApp.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public ObservableCollection<MenuItem> Menu { get; } = new ObservableCollection<MenuItem>();
        public ObservableCollection<MenuItem> OptionsMenu { get; } = new ObservableCollection<MenuItem>();


        public ShellViewModel()
        {
            // Build the menus
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconCoolicons() { Kind = PackIconCooliconsKind.Chat },
                Label = "Nhắn tin",
                NavigationType = typeof(ChatPage),
                NavigationDestination = new Uri("Views/ChatPage.xaml", UriKind.RelativeOrAbsolute)
            });
            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconVaadinIcons() { Kind = PackIconVaadinIconsKind.CogOutline },
                Label = "Cài đặt",
                NavigationType = typeof(SettingsPage),
                NavigationDestination = new Uri("Views/SettingsPage.xaml", UriKind.RelativeOrAbsolute)
            });
        }
    }
}