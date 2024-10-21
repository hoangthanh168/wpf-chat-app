using System;
using ChatApp.Mvvm;

namespace ChatApp.ViewModels
{
    public class MenuItem : BindableBase
    {
        private string _label;
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private string _icon;
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public Uri NavigationDestination { get; set; }

        private bool _isNavigation;
        public bool IsNavigation
        {
            get => _isNavigation;
            set => SetProperty(ref _isNavigation, value);
        }
    }
}