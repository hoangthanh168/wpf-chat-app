using ChatApp.ViewModels;
using ChatApp.Views;
using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ChatApp.Mvvm;
using MenuItem = ChatApp.ViewModels.MenuItem;
using System.Collections.Generic;

namespace ChatApp
{
    public partial class MainWindow : MetroWindow
    {
        private readonly Navigation.NavigationServiceEx navigationServiceEx;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the navigation service
            this.navigationServiceEx = new Navigation.NavigationServiceEx();
            this.navigationServiceEx.Navigated += NavigationServiceEx_OnNavigated;

            // Set up the Frame for navigation
            var frame = new Frame();
            this.navigationServiceEx.Frame = frame;
            this.Content = frame;

            // Navigate to the login page
            this.navigationServiceEx.Navigate(new Uri("Views/LoginPage.xaml", UriKind.RelativeOrAbsolute));

            // Set up the DataContext for the window
            var shellViewModel = new ShellViewModel();
            this.DataContext = shellViewModel;

            // Subscribe to login success event (you'll need to implement this)
            Messenger.Register<LoginSuccessMessage>(this, OnLoginSuccess);
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            if (e.InvokedItem is MenuItem menuItem && menuItem.IsNavigation)
            {
                this.navigationServiceEx.Navigate(menuItem.NavigationDestination);
            }
        }

        private void NavigationServiceEx_OnNavigated(object sender, NavigationEventArgs e)
        {
            // Update selected item in the HamburgerMenu
            if (this.HamburgerMenuControl.ItemsSource is IEnumerable<MenuItem> menuItems)
            {
                var selectedItem = menuItems.FirstOrDefault(x => x.NavigationDestination == e.Uri);
                if (selectedItem != null)
                {
                    this.HamburgerMenuControl.SelectedItem = selectedItem;
                }
            }

            // Update selected option item in the HamburgerMenu
            if (this.HamburgerMenuControl.OptionsItemsSource is IEnumerable<MenuItem> optionItems)
            {
                var selectedOptionItem = optionItems.FirstOrDefault(x => x.NavigationDestination == e.Uri);
                if (selectedOptionItem != null)
                {
                    this.HamburgerMenuControl.SelectedOptionsItem = selectedOptionItem;
                }
            }

            // Update back button visibility
            this.GoBackButton.Visibility = this.navigationServiceEx.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.navigationServiceEx.Navigate(new Uri("Views/SettingsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void OnLoginSuccess(LoginSuccessMessage message)
        {
            if (message.IsAdmin)
            {
                MessageBox.Show("Chào mừng Admin!");
                // Navigate to admin page if you have one
                // this.navigationServiceEx.Navigate(new Uri("Views/AdminPage.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                this.navigationServiceEx.Navigate(new Uri("Views/ChatPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void GoBack_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.navigationServiceEx.CanGoBack)
            {
                this.navigationServiceEx.GoBack();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Messenger.Unregister<LoginSuccessMessage>(this);
        }
    }
}