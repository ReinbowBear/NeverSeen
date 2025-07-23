namespace DialogueManager.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using DialogueManager.Models;

    public class ConversationController
    {
        private Conversation model;

        public ConversationController(Conversation newConversation)
        {
            newConversation.ActiveStatus = newConversation.Status[newConversation.ActiveStatusIndex];
            model = newConversation;
        }
        
        public void Trigger(DialogueData dialogueData) // этот класс что то добавляет в список разговоров..
        {
            Dictionary<string, List<PendingStatus>> conversations = model.GameConversations.PendingConversations;
            if (conversations.ContainsKey(model.Name) && conversations[model.Name].Count > 0)
            {
                List<PendingStatus> statusList = conversations[model.Name];
                string statusName = statusList[0].statusName;
                statusList.RemoveAt(0);

                model.ActiveStatus = model.Status.Where(status => status.Name.Equals(statusName)).First();
                model.ActiveStatusIndex = model.Status.IndexOf(model.ActiveStatus);
            }

            if (model.ActiveStatus != null)
            {
                TriggerStatus(dialogueData);
            }
        }

        /// <param name="dialogueData">Dialogue Manager where the Dialogue will be displayed</param>
        private void TriggerStatus(DialogueData dialogueData)
        {
            ConversationStatus status = model.ActiveStatus;
            model.GameConversations.ConversationsToAdd.AddRange(status.PendingConversations);

            dialogueData.DialogueToShow = status.Dialogue;
            model.ActiveStatus = status.NextStatus;
            model.ActiveStatusIndex = status.NextStatusIndex;
        }
    }
}
