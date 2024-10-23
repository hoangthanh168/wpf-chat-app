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
        public string AvatarPath { get; set; }


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

        public bool IsSentByCurrentUser
        {
            get => _isSentByCurrentUser;
            set => SetProperty(ref _isSentByCurrentUser, value);
        }

        public string SentAtFormatted => SentAt.ToString("HH:mm");

        public ICommand DeleteMessageCommand { get; private set; }

        public MessageItemViewModel()
        {
            DeleteMessageCommand = new DelegateCommand(DeleteMessage, CanDeleteMessage);
        }

        public MessageItemViewModel(string senderName, string content, DateTime sentAt, bool isSentByCurrentUser)
        {
            SenderName = senderName;
            Content = content;
            SentAt = sentAt;
            IsSentByCurrentUser = isSentByCurrentUser;

            DeleteMessageCommand = new DelegateCommand(DeleteMessage, CanDeleteMessage);
        }

        private void DeleteMessage()
        {
        }

        private bool CanDeleteMessage()
        {
            return IsSentByCurrentUser;
        }
    }
}
