using System.Windows.Controls;
using ChatApp.ViewModels;

namespace ChatApp.Views
{
    public partial class ChatView : UserControl
    {
        private ChatViewModel _chatViewModel;
        private bool _isAutoScrollEnabled = true;

        public ChatView(ChatViewModel chatViewModel)
        {
            InitializeComponent();
            this._chatViewModel = chatViewModel;
            DataContext = _chatViewModel;

            _chatViewModel.ScrollToEndRequested += ScrollToEndIfNeeded;
        }

        private void MessageList_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = e.OriginalSource as ScrollViewer;
            if (scrollViewer == null)
                return;

            // Kiểm tra xem người dùng có đang cuộn xuống cuối cùng không
            if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                _isAutoScrollEnabled = true; // Bật tự động cuộn khi người dùng đã ở cuối
            }
            else
            {
                _isAutoScrollEnabled = false; // Tắt tự động cuộn nếu người dùng không ở cuối
            }
        }

        // Hàm tự động cuộn khi nhận tin nhắn
        public void ScrollToEndIfNeeded(bool isForce)
        {
            if (isForce == true)
            {
                MessageList.ScrollIntoView(MessageList.Items[MessageList.Items.Count - 1]);
            }
            else
            {
                if (_isAutoScrollEnabled)
                {
                    MessageList.ScrollIntoView(MessageList.Items[MessageList.Items.Count - 1]);
                }
            }
        }
         
    }
}
