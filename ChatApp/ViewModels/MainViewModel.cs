using ChatApp.Mvvm;
using ChatApp.Views;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ViewModels
{
    public class MainViewModel : BindableBase
    {

        public ObservableCollection<MenuItem> Menu { get; }
        public ObservableCollection<MenuItem> OptionsMenu { get; }

        public MainViewModel()
        {
            Menu = new ObservableCollection<MenuItem>
            {
                new MenuItem
                {
                    Label = "Chat",
                    Icon = new PackIconBootstrapIcons() { Kind = PackIconBootstrapIconsKind.Chat },
                    NavigationDestination = new Uri("Views/ChatView.xaml", UriKind.Relative),
                    NavigationType = typeof(ChatView),
                }
            };

            OptionsMenu = new ObservableCollection<MenuItem>
            {
                new MenuItem
                {
                    Label = "Settings",
                    Icon = new PackIconUnicons() { Kind = PackIconUniconsKind.CogLine },
                    NavigationDestination = new Uri("Views/SettingsView.xaml", UriKind.Relative),
                    NavigationType = typeof(SettingsView),
                }
            };
        }

    }
}
