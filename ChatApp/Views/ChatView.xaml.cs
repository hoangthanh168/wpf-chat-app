using ChatApp.ViewModels;
using System.Windows.Controls;

namespace ChatApp.Views
{
    public partial class ChatView : UserControl
    {
        private ChatViewModel _chatViewModel;

        public ChatView(ChatViewModel chatViewModel)
        {
            InitializeComponent();
            this._chatViewModel = chatViewModel;
            DataContext = _chatViewModel;
        }

    }
}
