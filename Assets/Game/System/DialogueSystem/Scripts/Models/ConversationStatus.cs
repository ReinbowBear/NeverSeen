namespace DialogueManager.Models
{
    using System.Collections.Generic;

    [System.Serializable]
    public class ConversationStatus
    {
        public string Name;
        public int NextStatusIndex;
        public Dialogue Dialogue;
        public List<PendingStatus> PendingConversations;

        /// Gets or sets the <see cref="ConversationStatus"/> in which the Conversation will be once the Dialogue of the current Status ends.
        public ConversationStatus NextStatus { get; set; }
    }
}