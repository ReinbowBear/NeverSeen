namespace DialogueManager.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using DialogueManager.Models;

    /// <summary>
    /// Controller for the GameConversations Component
    /// </summary>
    public class GameConversationsController
    {
        private GameConversations model;

        public GameConversationsController( GameConversations gameConversations )
        {
            gameConversations.pendingConversations = new Dictionary<string, List<PendingStatus>>();
            gameConversations.ConversationsToAdd = new List<PendingStatus>();
            model = gameConversations;
        }

        // Creates a Key on the PendingConversations with the name of the Conversation if it doesn't exists already.
        // Adds the first element in ConversationsToAdd to the Value PendingConversations with the correct key and sorts the list.
        public void AddConversation()
        {
            PendingStatus unlockedStatus = model.ConversationsToAdd[0];
            model.ConversationsToAdd.RemoveAt( 0 );
            Dictionary<string, List<PendingStatus>> conversations = model.pendingConversations;
            if (!conversations.ContainsKey( unlockedStatus.conversationName ))
            {
                conversations[unlockedStatus.conversationName] = new List<PendingStatus>();
            }

            List<PendingStatus> pending = conversations[unlockedStatus.conversationName];
            PendingStatus match = pending.Where( status => status.conversationName == unlockedStatus.statusName ).FirstOrDefault();
            if (match == null)
            {
                pending.Add( unlockedStatus );
                pending.OrderBy( status => status.importance );
            }
        }
    }
}