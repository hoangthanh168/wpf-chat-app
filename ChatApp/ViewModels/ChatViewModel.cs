using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ChatApp.Core.Models;
using ChatApp.Core.Services;
using ChatApp.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows;

namespace ChatApp.ViewModels
{
    public class ChatViewModel : BindableBase
    {
        private ObservableCollection<ChatItemViewModel> _userGroupList;
        private ObservableCollection<MessageItemViewModel> _messageList;
        private string _messageInput;
        private readonly IServiceProvider _serviceProvider;
        private ChatItemViewModel _selectedChatItem;

        private ClientSocket _clientSocket;
        private UserService _userService;
        private UserSession _userSession;
        private MessageService _messageService;

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

        public ChatItemViewModel SelectedChatItem
        {
            get => _selectedChatItem;
            set
            {
                SetProperty(ref _selectedChatItem, value);
                LoadMessages();
            }
        }

        public ChatViewModel(IServiceProvider serviceProvider, ClientSocket clientSocket, UserSession userSession)
        {
            UserGroupList = new ObservableCollection<ChatItemViewModel>();
            MessageList = new ObservableCollection<MessageItemViewModel>();

            _serviceProvider = serviceProvider;

            _clientSocket = clientSocket;
            _userSession = userSession;
            _userService = _serviceProvider.GetRequiredService<UserService>();
            _messageService = _serviceProvider.GetRequiredService<MessageService>();

            CurrentUser = _userSession.CurrentUser;

            SendMessageCommand = new DelegateCommand(SendMessage, CanSendMessage);

            LoadUsers();

            if (_clientSocket != null)
            {
                _clientSocket.OnMessageReceived += HandleMessageReceived;
            }
        }

       

        private async void SendMessage()
        {
            if (_clientSocket != null && _clientSocket.IsConnected)
            {
                var sendMessageDTO = new ChatDTO.SendMessageDTO
                {
                    SenderId = CurrentUser.UserID,
                    ReceiverId = SelectedChatItem?.UserId,
                    GroupId = SelectedChatItem?.GroupId,
                    Content = MessageInput,
                    IsGroupMessage = SelectedChatItem?.IsGroup ?? false,
                    SentAt = DateTime.UtcNow,
                };

                var clientMessageDTO = new ChatDTO.ClientMessageDTO
                {
                    Type = ChatDTO.MessageType.SendMessage,
                    Payload = JObject.FromObject(sendMessageDTO),
                };

                try
                {
                    await _clientSocket.SendMessageAsync(clientMessageDTO);
                    MessageInput = string.Empty;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Gửi tin nhắn thất bại: " + ex.Message);
                }
            }
        }

        private bool CanSendMessage()
        {
            return !string.IsNullOrWhiteSpace(MessageInput) && SelectedChatItem != null;
        }

        private void LoadUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();

                UserGroupList.Clear();

                foreach (var user in users)
                {
                    if (user == _userSession.CurrentUser){
                        continue;
                    }

                    UserGroupList.Add(new ChatItemViewModel
                    {
                        UserId = user.UserID,
                        DisplayName = user.Username,
                        IsGroup = false
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load users: " + ex.Message);
            }
        }

        private void LoadMessages()
        {
            if (SelectedChatItem == null) return;

            try
            {
                MessageList.Clear();

                List<Message> currentUserMessages;
                List<Message> selectedUserMessages = new List<Message>();

                if (SelectedChatItem.IsGroup)
                {
                    currentUserMessages = _messageService.GetMessagesByGroupId(SelectedChatItem.GroupId);
                }
                else
                {
                    currentUserMessages = _messageService.GetPrivateMessages(CurrentUser.UserID, SelectedChatItem.UserId);
                    selectedUserMessages = _messageService.GetPrivateMessages(SelectedChatItem.UserId, CurrentUser.UserID);

                    currentUserMessages.AddRange(selectedUserMessages);
                }

                foreach (var message in currentUserMessages)
                {
                    MessageList.Add(new MessageItemViewModel
                    {
                        SenderName = message.Sender.Username,
                        Content = message.Content,
                        SentAt = message.SentAt,
                        IsSentByCurrentUser = message.SenderID == CurrentUser.UserID
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load messages: " + ex.Message);
            }

        }


        private void HandleMessageReceived(ChatDTO.ServerMessageDTO serverMessage)
        {
            if (serverMessage == null)
            {
                return;
            }

            try
            {
                switch (serverMessage.Type)
                {
                    case ChatDTO.MessageType.SendMessage:
                        var messagePayload = serverMessage.Payload as JObject;
                        if (messagePayload != null)
                        {
                            var sendMessageDTO = messagePayload.ToObject<ChatDTO.SendMessageDTO>();
                            HandleIncomingMessage(sendMessageDTO);
                        }
                        break;

                    case ChatDTO.MessageType.Acknowledgment:
                        var acknowledgmentPayload = serverMessage.Payload as JObject;
                        if (acknowledgmentPayload != null)
                        {
                            var acknowledgment = acknowledgmentPayload.ToObject<ChatDTO.AcknowledgmentDTO>();
                            Console.WriteLine($"Acknowledgment received: {acknowledgment.Message}");
                        }
                        break;

                    default:
                        Console.WriteLine($"Received unknown message type: {serverMessage.Type}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling received message: {ex.Message}");
            }
        }


        private void HandleIncomingMessage(ChatDTO.SendMessageDTO message)
        {
            if (message == null)
            {
                return;
            }

            bool isForCurrentChat = SelectedChatItem?.UserId == message.SenderId;

            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageList.Add(new MessageItemViewModel
                {
                    SenderName = GetSenderName(message.SenderId),
                    Content = message.Content,
                    SentAt = message.SentAt,
                    IsSentByCurrentUser = message.SenderId == CurrentUser.UserID
                });
            });
        }


        private string GetSenderName(int senderId)
        {
            // Tìm tên người gửi dựa trên senderId
            var user = _userService.GetUserById(senderId);
            return user?.Username ?? "Unknown";
        }

    }
}
