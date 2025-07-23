namespace DialogueManager.GameComponents
{
    using DialogueManager.Controllers;
    using DialogueManager.Models;
    using UnityEngine;
    using UnityEngine.UI;

    // This class manages the text in the dialogues, the transition between sentences, animations, and such
    public class DialogueManagerComponent : MonoBehaviour
    {
        public static DialogueManagerComponent Instance;
        public DialogueData Model;
        private DialogueManagerController controller;
        
        [SerializeField] private Transform dialogueStartPoint;
        [SerializeField] private Image characterImage;
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource audioSource;

        private void Awake()
        {
            GameObject gameConversations = Instantiate(Model.GameConversationsPrefab); // откуда переменная заполнена у модели?
            gameConversations.name = "GameConversations";

            Model.DialogueStartPoint = dialogueStartPoint;
            Model.ImageText = characterImage;
            Model.Animator = animator;
            Model.Source = audioSource; // по идеи у нас глобальная точка звука кста

            controller = new DialogueManagerController(Model);
        }

        void Update()
        {
            if (Model.DialogueToShow != null)
            {
                StartDialogue();
            }

            if (Input.GetKeyDown(Model.NextKey) && Model.Finished && Model.DoubleTap)
            {
                DisplayNextSentence();
                Model.Finished = false;
            }

            if (Input.GetKeyDown(Model.NextKey) && Model.DoubleTap == false)
            {
                Model.Finished = true;
                DisplayNextSentence();
            }
        }


        private void StartDialogue() // очевидно этот класс для взаимодействия игрока с диалогом
        {
            controller.StartDialogue();
            DisplayNextSentence();
        }

        private void DisplayNextSentence()
        {
            StopAllCoroutines();
            if (controller.DisplayNextSentence())
            {
                StartCoroutine(controller.TypeSentence());
            }
        }
    }
}