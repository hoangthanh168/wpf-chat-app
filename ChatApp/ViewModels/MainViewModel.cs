using ChatApp.Mvvm;
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
                    NavigationDestination = new Uri("Views/ChatPage.xaml", UriKind.Relative)
                }
            };

            OptionsMenu = new ObservableCollection<MenuItem>
            {
                new MenuItem
                {
                    Label = "Settings",
                    Icon = new PackIconUnicons() { Kind = PackIconUniconsKind.CogLine },
                    NavigationDestination = new Uri("Views/SettingsPage.xaml", UriKind.Relative)
                }
            };
        }

    }
}
