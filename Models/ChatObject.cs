using System.Text.Json.Serialization;

namespace FunctionApp.Models
{
    public class ChatObject
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        public ChatObject() { }

        public ChatObject(string role, string content)
        {
            Role = role;
            Content = content;
        }

        // Helper: System prompt (dynamic!)
        public static ChatObject CreateSystemMessage(string content)
            => new ChatObject("system", content);

        // Helper: User
        public static ChatObject CreateUserMessage(string content)
            => new ChatObject("user", content);

        // Helper: Assistant
        public static ChatObject CreateAssistantMessage(string content)
            => new ChatObject("assistant", content);
    }
}
