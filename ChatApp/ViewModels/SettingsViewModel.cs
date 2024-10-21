using System.Windows.Input;
using ChatApp.Mvvm;

namespace ChatApp.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        private bool _enableNotifications;
        public bool EnableNotifications
        {
            get => _enableNotifications;
            set => SetProperty(ref _enableNotifications, value);
        }

        private string _selectedTheme;
        public string SelectedTheme
        {
            get => _selectedTheme;
            set => SetProperty(ref _selectedTheme, value);
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set => SetProperty(ref _selectedLanguage, value);
        }

        public ICommand SaveCommand { get; }

        public SettingsViewModel()
        {
            // Initialize with default values or load from settings
            EnableNotifications = true;
            SelectedTheme = "Light";
            SelectedLanguage = "English";

            SaveCommand = new DelegateCommand(SaveSettings);
        }

        private void SaveSettings()
        {
            // Implement logic to save settings
            // For example:
            // SaveSettingsToFile();
            // or
            // UpdateSettingsInDatabase();
        }
    }
}