using ChatApp.Navigation;
using ChatApp.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;
using MenuItem = ChatApp.ViewModels.MenuItem;

namespace ChatApp.Views
{
    public partial class MainView : UserControl
    {
        private readonly NavigationServiceEx _navigationServiceEx;
        private readonly MainViewModel _viewModel;

        public MainView()
        {
                
        }
        public MainView(MainViewModel viewModel, NavigationServiceEx navigationServiceEx)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;

            _navigationServiceEx = navigationServiceEx;
            this._navigationServiceEx.Navigated += this.NavigationServiceEx_OnNavigated;
            this.HamburgerMenuControl.Content = this._navigationServiceEx.Frame;

            Loaded += (sender, args) => _navigationServiceEx.Navigate(new Uri("Views/ChatPage.xaml", UriKind.Relative));
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            if (e.InvokedItem is MenuItem menuItem && menuItem.IsNavigation)
            {
                _navigationServiceEx.Navigate(menuItem.NavigationDestination);
            }
        }

        private void NavigationServiceEx_OnNavigated(object sender, NavigationEventArgs e)
        {
            if (_viewModel.Menu != null)
            {
                var selectedItem = _viewModel.Menu.FirstOrDefault(x => x.NavigationDestination == e.Uri);
                if (selectedItem != null)
                {
                    HamburgerMenuControl.SelectedItem = selectedItem;
                }
            }

            if (_viewModel.OptionsMenu != null)
            {
                var selectedOptionItem = _viewModel.OptionsMenu.FirstOrDefault(x => x.NavigationDestination == e.Uri);
                if (selectedOptionItem != null)
                {
                    HamburgerMenuControl.SelectedOptionsItem = selectedOptionItem;
                }
            }

        }
    }
}
