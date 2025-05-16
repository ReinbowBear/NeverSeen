namespace DialogueManager.GameComponents
{
    using DialogueManager.Controllers;
    using DialogueManager.Models;
    using UnityEngine;

    /// <summary>
    /// Component of all the Pending Conversations in the Game
    /// </summary>
    public class GameConversationsComponent : MonoBehaviour
    {
        public GameConversations Model;
        private GameConversationsController controller;

        private void Awake()
        {
            if (Model == null)
            {
                Model = new GameConversations();
            }

            controller = new GameConversationsController(Model);
        }

        private void Update()
        {
            if (Model.ConversationsToAdd.Count > 0)
            {
                AddConversation();
            }
        }

        private void AddConversation()
        {
            controller.AddConversation();
        }
    }
}