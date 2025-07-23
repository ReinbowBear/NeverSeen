namespace DialogueManager.Models
{
    using System.Collections.Generic;

    public class GameConversations
    {
        public Dictionary<string, List<PendingStatus>> PendingConversations { get; set; }
        public List<PendingStatus> ConversationsToAdd { get; set; }
    }


    [System.Serializable]
    public class PendingStatus
    {
        public string conversationName;
        public string statusName;
        public int importance;
    }
}