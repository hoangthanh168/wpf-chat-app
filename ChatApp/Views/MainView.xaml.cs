using ChatApp.Navigation;
using ChatApp.ViewModels;
using MahApps.Metro.Controls;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;
using MenuItem = ChatApp.ViewModels.MenuItem;

namespace ChatApp.Views
{
    public partial class MainView : UserControl
    {
        private readonly NavigationServiceEx _navigationServiceEx;
        private readonly MainViewModel _mainViewModel;

        public MainView(MainViewModel mainViewModel, NavigationServiceEx navigationServiceEx)
        {
            InitializeComponent();
            _mainViewModel = mainViewModel;
            DataContext = _mainViewModel;

            _navigationServiceEx = navigationServiceEx;
            this._navigationServiceEx.Navigated += this.NavigationServiceEx_OnNavigated;
            this.HamburgerMenuControl.Content = this._navigationServiceEx.Frame;

            Loaded += (sender, args) => _navigationServiceEx.Navigate<ChatView>();
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            if (e.InvokedItem is MenuItem menuItem && menuItem.IsNavigation)
            {
                if (menuItem.NavigationType != null)
                {
                    _navigationServiceEx.Navigate(menuItem.NavigationType);
                }
            }
        }

        private void NavigationServiceEx_OnNavigated(object sender, NavigationEventArgs e)
        {
            if (_mainViewModel.Menu != null)
            {
                var selectedItem = _mainViewModel.Menu.FirstOrDefault(x => x.NavigationDestination == e.Uri);
                if (selectedItem != null)
                {
                    HamburgerMenuControl.SelectedItem = selectedItem;
                }
            }

            if (_mainViewModel.OptionsMenu != null)
            {
                var selectedOptionItem = _mainViewModel.OptionsMenu.FirstOrDefault(x => x.NavigationDestination == e.Uri);
                if (selectedOptionItem != null)
                {
                    HamburgerMenuControl.SelectedOptionsItem = selectedOptionItem;
                }
            }

        }
    }
}
