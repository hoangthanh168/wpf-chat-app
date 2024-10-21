using ChatApp.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ViewModels
{
    public class ChatItemViewModel : BindableBase
    {
        private string _displayName;
        private string _lastMessage;
        private bool _isSelected;

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

        // Thuộc tính này giúp xác định item hiện tại có được chọn hay không
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        // Bạn có thể thêm command hoặc logic khác nếu cần thiết
    }
}
 