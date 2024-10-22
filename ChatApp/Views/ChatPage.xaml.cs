using ChatApp.ViewModels;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace ChatApp.Views
{
    public partial class ChatPage : UserControl
    {
        private ChatViewModel _viewModel;

        public ChatPage(ChatViewModel viewModel)
        {
            InitializeComponent();
            this._viewModel = viewModel;
        }

    }
}
