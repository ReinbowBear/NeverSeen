namespace DialogueManager.Models
{
    using System.Collections.Generic;

    [System.Serializable]
    public class Conversation
    {
        public string Name;
        public List<ConversationStatus> Status;
        public ConversationStatus ActiveStatus;
        public int ActiveStatusIndex;

        public GameConversations GameConversations { get; set; }
    }
}