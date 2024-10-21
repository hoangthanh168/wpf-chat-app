using ChatApp.ViewModels;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace ChatApp.Views
{
    public partial class ChatPage : UserControl
    {
        private ChatViewModel viewModel;

        public ChatPage()
        {
            InitializeComponent();
            // Lắng nghe sự thay đổi của MessageList khi DataContext đã sẵn sàng
            this.Loaded += ChatPage_Loaded;
        }

        private void ChatPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Đảm bảo rằng viewModel được gán đúng từ DataContext
            viewModel = DataContext as ChatViewModel;

            if (viewModel != null)
            {
                // Lắng nghe sự thay đổi của MessageList
                viewModel.MessageList.CollectionChanged += MessageList_CollectionChanged;
            }
        }

        private void MessageList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // Cuộn đến tin nhắn mới nhất
                if (MessageList.Items.Count > 0)
                {
                    var lastItem = MessageList.Items[MessageList.Items.Count - 1];
                    MessageList.ScrollIntoView(lastItem);
                }
            }
        }
        private void MessageInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                // Gọi command gửi tin nhắn khi nhấn Enter
                if (viewModel.SendMessageCommand.CanExecute(null))
                {
                    viewModel.SendMessageCommand.Execute(null);
                }

                // Ngăn việc xuống dòng sau khi nhấn Enter
                e.Handled = true;
            }
        }

    }
}
