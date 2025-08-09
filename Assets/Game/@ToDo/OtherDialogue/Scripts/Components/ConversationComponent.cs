namespace DialogueManager.GameComponents
{
    using DialogueManager.Controllers;
    using DialogueManager.Models;
    using UnityEngine;

    // Conversation Component, must be added for every single NPC or Situation that has a Conversation
    public class ConversationComponent : MonoBehaviour
    {
        public Conversation Model;
        private ConversationController controller;

        private void Awake()
        {
            controller = new ConversationController(Model);
        }

        public void Trigger()
        {
            Model.GameConversations = GameConversationsController.Instance.Model; // а что зачем тут делается
            DialogueData dialogueData = DialogueManagerComponent.Instance.Model;
            controller.Trigger(dialogueData);
        }
    }
}