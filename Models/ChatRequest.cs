using System.Collections.Generic;
using System.Text;
using FunctionApp.Helpers;

// Models an API request body containing a list of chat messages (chat history)
// Contains Sanitize Method to verify integrity of Chat Request Object sent from front end

namespace FunctionApp.Models
{

    public class ChatRequest
    {
        public List<ChatObject> Messages { get; set; }
    
        public void SanitizeAndValidateMessages()
        {
            if (Messages == null || Messages.Count == 0)
                throw new ArgumentException("Messages array is required and cannot be empty.");

            foreach (var msg in Messages)
            {
                if (msg == null)
                    throw new ArgumentException("All messages must be non-null.");

                msg.Content = CleanControlCharacters.ReplaceControlCharacters(msg.Content);

                if (string.IsNullOrWhiteSpace(msg.Content))
                    throw new ArgumentException("All messages must have content after sanitization.");

                if (msg.Role != "user" && msg.Role != "assistant")
                    throw new ArgumentException("Only 'user' and 'assistant' roles are allowed. 'system' prompts are not permitted.");
            }
        }
}}
