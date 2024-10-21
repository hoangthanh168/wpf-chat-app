using ChatApp.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ChatApp.ViewModels
{
    // Định nghĩa lớp User nếu chưa có
    public class User
    {
        public string Name { get; set; }
    }

    public class ChatViewModel : BindableBase
    {
        private ObservableCollection<ChatItemViewModel> _userGroupList;
        private ObservableCollection<MessageItemViewModel> _messageList;
        private string _messageInput;

        public ObservableCollection<ChatItemViewModel> UserGroupList
        {
            get => _userGroupList;
            set => SetProperty(ref _userGroupList, value);
        }

        public ObservableCollection<MessageItemViewModel> MessageList
        {
            get => _messageList;
            set => SetProperty(ref _messageList, value);
        }

        public string MessageInput
        {
            get => _messageInput;
            set => SetProperty(ref _messageInput, value);
        }

        public ICommand SendMessageCommand { get; set; }

        // Giả sử đây là người dùng hiện tại
        public User CurrentUser { get; set; }

        public ChatViewModel()
        {
            // Khởi tạo các collection
            UserGroupList = new ObservableCollection<ChatItemViewModel>();
            MessageList = new ObservableCollection<MessageItemViewModel>();

            // Khởi tạo người dùng hiện tại
            CurrentUser = new User { Name = "Bạn" };

            // Thêm dữ liệu mẫu vào danh sách người dùng nhóm
            UserGroupList.Add(new ChatItemViewModel
            {
                DisplayName = "Nhóm Bạn Đá Banh",
                LastMessage = "Hello."
            });
            UserGroupList.Add(new ChatItemViewModel
            {
                DisplayName = "Ngô Võ Hùng Thịnh",
                LastMessage = "Mua đồ ăn đi..."
            });

            // Thêm dữ liệu mẫu vào danh sách tin nhắn
            AddSampleMessages();

            // Khởi tạo command gửi tin nhắn
            SendMessageCommand = new DelegateCommand(SendMessage, CanSendMessage);
        }

        private void AddSampleMessages()
        {
            // Tin nhắn từ người khác
            MessageList.Add(new MessageItemViewModel(
                senderName: "Alice",
                content: "Chào bạn! Bạn khỏe không?",
                sentAt: DateTime.Now.AddMinutes(-30),
                isSentByCurrentUser: false
            ));

            MessageList.Add(new MessageItemViewModel(
                senderName: "Bob",
                content: "Hẹn gặp bạn vào cuối tuần nhé!",
                sentAt: DateTime.Now.AddMinutes(-25),
                isSentByCurrentUser: false
            ));

            // Tin nhắn từ người dùng hiện tại
            MessageList.Add(new MessageItemViewModel(
                senderName: CurrentUser.Name,
                content: "Mình khỏe, cảm ơn Alice!",
                sentAt: DateTime.Now.AddMinutes(-20),
                isSentByCurrentUser: true
            ));

            MessageList.Add(new MessageItemViewModel(
                senderName: CurrentUser.Name,
                content: "Vâng, hẹn gặp bạn vào cuối tuần!",
                sentAt: DateTime.Now.AddMinutes(-15),
                isSentByCurrentUser: true
            ));
        }

        private void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(MessageInput))
                return;

            // Tạo tin nhắn mới từ người dùng hiện tại
            var message = new MessageItemViewModel(
                senderName: CurrentUser.Name,
                content: MessageInput,
                sentAt: DateTime.Now,
                isSentByCurrentUser: true
            );

            // Thêm tin nhắn vào danh sách
            MessageList.Add(message);

            // Xóa nội dung nhập sau khi gửi
            MessageInput = string.Empty;
        }

        private bool CanSendMessage()
        {
            return !string.IsNullOrWhiteSpace(MessageInput);
        }
    }
}
