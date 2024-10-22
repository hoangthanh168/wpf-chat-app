using ChatApp.Core.Models;
using ChatApp.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ChatApp.ViewModels
{

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
            set
            {
                SetProperty(ref _messageInput, value);
                ((DelegateCommand)SendMessageCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand SendMessageCommand { get; set; }

        public User CurrentUser { get; set; }

        public ChatViewModel()
        {
            UserGroupList = new ObservableCollection<ChatItemViewModel>();
            MessageList = new ObservableCollection<MessageItemViewModel>();

            CurrentUser = new User { };

            SendMessageCommand = new DelegateCommand(SendMessage, CanSendMessage);
        }

        private void SendMessage()
        {
        }

        private bool CanSendMessage()
        {
            return !string.IsNullOrWhiteSpace(MessageInput);
        }
    }

}
