using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionApp.Models
{
    public class ChatObject
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("tool_call_id")]
        
        public string ToolCallId { get; set; }

        [JsonPropertyName("tool_calls")]
        public List<JsonElement>? ToolCalls { get; set; }
        public ChatObject() { }

        public ChatObject(string role, string content, string? toolCallId = null)
        {
            Role = role;
            Content = content;
            ToolCallId = toolCallId;
            ToolCalls = ToolCalls;
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

        //helper to correctly insert response from MCP in correct index of Messages list before sending to API
        public static void InsertToolResponse(List<ChatObject> messages, ChatObject toolMsg)
        {
            // Find the last assistant message with content == null (i.e., it's a tool_call placeholder)
            int idx = messages.FindLastIndex(m => string.Equals(m.Role, "assistant", StringComparison.OrdinalIgnoreCase) && m.Content == null);
            if (idx == -1)
                messages.Add(toolMsg); // fallback, should never happen
            else
                messages.Insert(idx + 1, toolMsg);
        }
    }
}
