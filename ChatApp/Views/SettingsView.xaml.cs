using ChatApp.ViewModels;
using System.Windows.Controls;

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