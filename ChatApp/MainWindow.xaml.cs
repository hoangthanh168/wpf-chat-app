using ChatApp.ViewModels;
using ChatApp.Views;
using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MenuItem = ChatApp.ViewModels.MenuItem;

namespace ChatApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly Navigation.NavigationServiceEx navigationServiceEx;

        public MainWindow()
        {
            InitializeComponent();

            var loginPage = new LoginPage();
            var loginViewModel = new LoginViewModel();
            loginPage.DataContext = loginViewModel;

            // Đặt trang LoginPage là trang đầu tiên
            MainFrame.Content = loginPage;

            loginViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(loginViewModel.IsLoginSuccessful) && loginViewModel.IsLoginSuccessful)
                {
                    if (loginViewModel.IsAdmin)
                    {
                        MessageBox.Show("Chào mừng Admin!");
                    }
                    else
                    {
                        // Điều hướng đến trang ChatPage
                        var chatViewModel = new ChatViewModel { CurrentUser = new User { Name = loginViewModel.Name } };
                        var chatPage = new ChatPage { DataContext = chatViewModel };
                        MainFrame.Navigate(chatPage);
                    }
                }
            };
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
            // select the menu item
            this.HamburgerMenuControl.SetCurrentValue(HamburgerMenu.SelectedItemProperty,
                this.HamburgerMenuControl.Items
                    .OfType<MenuItem>()
                    .FirstOrDefault(x => x.NavigationDestination == e.Uri));
            this.HamburgerMenuControl.SetCurrentValue(HamburgerMenu.SelectedOptionsItemProperty,
                this.HamburgerMenuControl
                    .OptionsItems
                    .OfType<MenuItem>()
                    .FirstOrDefault(x => x.NavigationDestination == e.Uri));

            // or when using the NavigationType on menu item
            // this.HamburgerMenuControl.SelectedItem = this.HamburgerMenuControl
            //                                              .Items
            //                                              .OfType<MenuItem>()
            //                                              .FirstOrDefault(x => x.NavigationType == e.Content?.GetType());
            // this.HamburgerMenuControl.SelectedOptionsItem = this.HamburgerMenuControl
            //                                                     .OptionsItems
            //                                                     .OfType<MenuItem>()
            //                                                     .FirstOrDefault(x => x.NavigationType == e.Content?.GetType());

            // update back button
            this.GoBackButton.SetCurrentValue(VisibilityProperty, this.navigationServiceEx.CanGoBack ? Visibility.Visible : Visibility.Collapsed);
        }

        
        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.navigationServiceEx.Navigate(new Uri("Views/SettingsPage.xaml", UriKind.RelativeOrAbsolute));
        }
       
        private void OnLoginSuccess(string userName, bool isAdmin)
        {
            if (isAdmin)
            {
                // Mở trang hoặc cung cấp quyền truy cập cho admin
                MessageBox.Show("Chào mừng Admin!");
                // Có thể điều hướng đến trang quản trị
            }
            else
            {
                // Khởi tạo trang chat cho người dùng thường
                var chatViewModel = new ChatViewModel
                {
                    CurrentUser = new User { Name = userName }
                };

                var chatPage = new ChatPage();
                chatPage.DataContext = chatViewModel;

                // Điều hướng tới trang Chat
                this.navigationServiceEx.Navigate(new Uri("Views/ChatPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }
        private void GoBack_OnClick(object sender, RoutedEventArgs e)
        {
            // Kiểm tra nếu có thể quay lại trang trước
            if (this.navigationServiceEx.CanGoBack)
            {
                this.navigationServiceEx.GoBack();
            }
        }


    }
}
