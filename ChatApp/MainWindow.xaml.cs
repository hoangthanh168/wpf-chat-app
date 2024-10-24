using MahApps.Metro.Controls;
using ChatApp.ViewModels;

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
