using ChatApp;
using ChatApp.Core.Models;
using ChatApp.Core.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var client = new ClientSocket();
            client.OnConnected += () => Console.WriteLine("Connected to the server.");
            client.OnDisconnected += () => Console.WriteLine("Disconnected from the server.");
            client.OnMessageReceived += HandleIncomingMessage;

            try
            {
                await client.ConnectAsync("127.0.0.1", 5000);
                var passwordHash = Security.Encrypt("hoangthanh123");
                var loginDto = new ChatDTO.ClientMessageDTO
                {
                    Type = ChatDTO.MessageType.Login,

                    Payload = JObject.FromObject(new ChatDTO.LoginRequestDTO
                    {
                        Username = "hoangthanh123",
                        PasswordHash = passwordHash
                    })
                };
                await client.SendMessageAsync(loginDto);

                Console.WriteLine("You can start sending messages. Type 'exit' to quit.");
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input.ToLower() == "exit")
                        break;

                    // For simplicity, send all messages as broadcast
                    var sendMessageDto = new ChatDTO.ClientMessageDTO
                    {
                        Type = ChatDTO.MessageType.SendMessage,
                        Payload = JObject.FromObject(new ChatDTO.SendMessageDTO
                        {
                            SenderId = new Random().Next(1, 10),
                            ReceiverId = 0,
                            Content = input,
                            IsGroupMessage = false,
                            SentAt = DateTime.UtcNow
                        })
                    };
                    await client.SendMessageAsync(sendMessageDto);
                }

                client.Disconnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        static void HandleIncomingMessage(ChatDTO.ServerMessageDTO serverMessage)
        {
            switch (serverMessage.Type)
            {
                case ChatDTO.MessageType.LoginResponse:
                    var loginResponse = ((JObject)serverMessage.Payload).ToObject<ChatDTO.LoginResponseDTO>();
                    Console.WriteLine($"Login Success: {loginResponse.Success}, Message: {loginResponse.Message}");
                    break;

                case ChatDTO.MessageType.SendMessage:
                    var chatMessage = ((JObject)serverMessage.Payload).ToObject<ChatDTO.SendMessageDTO>();
                    Console.WriteLine($"From User {chatMessage.SenderId}: {chatMessage.Content}");
                    break;

                default:
                    Console.WriteLine("Unknown message type received.");
                    break;
            }
        }
    }
}
