using Newtonsoft.Json.Linq;
using System;

namespace ChatApp.Core.Models
{
    public class ChatDTO
    {
        public enum MessageType
        {
            Login,
            LoginResponse,
            SendMessage,
            Acknowledgment
        }

        public class ClientMessageDTO
        {
            public MessageType Type { get; set; }
            public JObject Payload { get; set; }
        }

        public class ServerMessageDTO
        {
            public MessageType Type { get; set; }
            public object Payload { get; set; }
        }

        public class LoginRequestDTO
        {
            public string Username { get; set; }
            public string PasswordHash { get; set; }
        }

        public class LoginResponseDTO
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public int UserId { get; set; }
        }

        public class SendMessageDTO
        {
            public int SenderId { get; set; }
            public int? ReceiverId { get; set; }
            public int? GroupId { get; set; }
            public string Content { get; set; }
            public bool IsGroupMessage { get; set; }
            public DateTime SentAt { get; set; }
        }

        public class AcknowledgmentDTO
        {
            public string Message { get; set; }
        }
    }
}
