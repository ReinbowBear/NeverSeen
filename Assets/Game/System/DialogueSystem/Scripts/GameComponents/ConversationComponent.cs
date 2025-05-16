namespace DialogueManager.GameComponents
{
    using DialogueManager.Controllers;
    using DialogueManager.Models;
    using UnityEngine;

    /// <summary>
    /// Conversation Component, must be added for every single NPC or Situation that has a Conversation
    /// </summary>
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
            Model.GameConversations = GameObject.Find( "GameConversations" ).GetComponent<GameConversationsComponent>().Model;
            DialogueData dialogueManager = GameObject.Find( "DialogueManager" ).GetComponent<DialogueManagerComponent>().Model;
            controller.Trigger( dialogueManager );
        }
    }
}