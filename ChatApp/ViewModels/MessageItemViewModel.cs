using ChatApp.Mvvm;
using System;
using System.Windows.Input;

namespace ChatApp.ViewModels
{
    public class MessageItemViewModel : BindableBase
    {
        private string _senderName;
        private string _content;
        private DateTime _sentAt;
        private bool _isSentByCurrentUser;
        public string AvatarPath { get; set; } // Đường dẫn đến hình ảnh avatar


        public string SenderName
        {
            get => _senderName;
            set => SetProperty(ref _senderName, value);
        }

        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        public DateTime SentAt
        {
            get => _sentAt;
            set => SetProperty(ref _sentAt, value);
        }

        // Thuộc tính giúp xác định tin nhắn do người dùng hiện tại gửi
        public bool IsSentByCurrentUser
        {
            get => _isSentByCurrentUser;
            set => SetProperty(ref _isSentByCurrentUser, value);
        }

        // Định dạng thời gian gửi tin nhắn
        public string SentAtFormatted => SentAt.ToString("HH:mm");

        // Command để xóa tin nhắn
        public ICommand DeleteMessageCommand { get; private set; }

        // Constructor mặc định (nếu cần)
        public MessageItemViewModel()
        {
            DeleteMessageCommand = new DelegateCommand(DeleteMessage, CanDeleteMessage);
        }

        // Constructor có tham số
        public MessageItemViewModel(string senderName, string content, DateTime sentAt, bool isSentByCurrentUser)
        {
            SenderName = senderName;
            Content = content;
            SentAt = sentAt;
            IsSentByCurrentUser = isSentByCurrentUser;

            DeleteMessageCommand = new DelegateCommand(DeleteMessage, CanDeleteMessage);
        }

        // Logic để xóa tin nhắn
        private void DeleteMessage()
        {
            // Xóa tin nhắn logic (ở đây bạn có thể thêm logic xóa từ danh sách)
            Console.WriteLine($"Message '{Content}' has been deleted.");
        }

        // Kiểm tra nếu tin nhắn do người dùng hiện tại gửi thì cho phép xóa
        private bool CanDeleteMessage()
        {
            return IsSentByCurrentUser;
        }
    }
}
