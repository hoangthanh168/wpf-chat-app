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
        public ObservableCollection<MenuItem> Menu { get; }
        public ObservableCollection<MenuItem> OptionsMenu { get; }

        public ShellViewModel()
        {
            Menu = new ObservableCollection<MenuItem>
            {
                new MenuItem { Label = "Home", NavigationDestination = new System.Uri("Views/HomePage.xaml", System.UriKind.Relative) },
                new MenuItem { Label = "Settings", NavigationDestination = new System.Uri("Views/SettingsPage.xaml", System.UriKind.Relative) }
            };

            OptionsMenu = new ObservableCollection<MenuItem>
            {
                new MenuItem { Label = "About", NavigationDestination = new System.Uri("Views/AboutPage.xaml", System.UriKind.Relative) }
            };
        }
    }

    public class MenuItem : BindableBase
    {
        public string Label { get; set; }
        public object Icon { get; set; }
        public System.Uri NavigationDestination { get; set; }
    }
}
