using System.Windows.Controls;
using ChatApp.ViewModels;

namespace ChatApp.Views
{
    public partial class SettingsView : UserControl
    {
        private readonly SettingsViewModel _settingsViewModel;

        public SettingsView(SettingsViewModel settingsViewModel)
        {
            InitializeComponent();
            this._settingsViewModel = settingsViewModel;
            DataContext = settingsViewModel;
        }
    }
}