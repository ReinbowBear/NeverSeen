namespace DialogueManager.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using DialogueManager.Models;
    using UnityEngine;

    public class GameConversationsController : MonoBehaviour // Controller for the GameConversations Component
    {
        public static GameConversationsController Instance;
        public GameConversations Model;

        void Awake()
        {
            Instance = this;

            Model = new GameConversations();
            Model.PendingConversations = new Dictionary<string, List<PendingStatus>>();
            Model.ConversationsToAdd = new List<PendingStatus>();
        }

        public void AddConversation() // if (Model.ConversationsToAdd.Count > 0)
        {
            PendingStatus unlockedStatus = Model.ConversationsToAdd[0];
            Model.ConversationsToAdd.RemoveAt(0);

            if (Model.PendingConversations.ContainsKey(unlockedStatus.conversationName) == false)
            {
                Model.PendingConversations[unlockedStatus.conversationName] = new List<PendingStatus>();
            }

            List<PendingStatus> pending = Model.PendingConversations[unlockedStatus.conversationName];
            PendingStatus match = pending.Where(status => status.conversationName == unlockedStatus.statusName).FirstOrDefault();
            if (match == null)
            {
                pending.Add(unlockedStatus);
                pending.OrderBy(status => status.importance);
            }
        }
    }
}