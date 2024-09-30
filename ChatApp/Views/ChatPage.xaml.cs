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
            viewModel = new ChatViewModel();
            this.DataContext = viewModel;

            // Lắng nghe sự thay đổi của MessageList
            viewModel.MessageList.CollectionChanged += MessageList_CollectionChanged;
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
    }
}
