using ChatApp.ViewModels;
using MahApps.Metro.Controls;

namespace ChatApp
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(ShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
