using System.Windows.Controls;
using ChatApp.ViewModels;

namespace ChatApp.Views
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            DataContext = new SettingsViewModel();
        }
    }
}