using ChatApp.Mvvm;

namespace ChatApp.ViewModels
{
    public class ChatItemViewModel : BindableBase
    {
        private string _displayName;
        private string _lastMessage;
        private bool _isSelected;
        private int _userId;
        private int _groupId;
        private bool _isGroup;

        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        public string LastMessage
        {
            get => _lastMessage;
            set => SetProperty(ref _lastMessage, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        public int GroupId
        {
            get => _groupId;
            set => SetProperty(ref _groupId, value);
        }

        public bool IsGroup
        {
            get => _isGroup;
            set => SetProperty(ref _isGroup, value);
        }

        public ChatItemViewModel()
        {

        }
    }
}
