namespace DialogueManager.GameComponents
{
    using DialogueManager.Controllers;
    using DialogueManager.Models;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// This class manages the text in the dialogues, the transition between sentences, animations, and such
    /// </summary>
    public class DialogueManagerComponent : MonoBehaviour
    {
        public DialogueData Model;
        private DialogueManagerController controller;

        private void Awake()
        {
            GameObject gameConversations = Instantiate( Model.GameConversationsPrefab );
            gameConversations.name = "GameConversations";

            /*
            GameObject canvasObject = new GameObject( "DialogueCanvas", typeof(RectTransform) );
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            */
            Transform canvasObject = GameObject.Find( "DialogueCanvas" ).GetComponent<Transform>();
            GameObject dialogueBox = Instantiate( Model.CanvasObjectsPrefab );
            dialogueBox.transform.position = new Vector3( -250, 0, 0 );
            dialogueBox.name = "DialogueBox";
            dialogueBox.transform.SetParent( canvasObject.transform );
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector3( 0, -500, 0 );


            Model.DialogueStartPoint = GameObject.Find( "/DialogueCanvas/DialogueBox/DialogueStartPoint" ).GetComponent<Transform>();
            Model.ImageText = GameObject.Find( "/DialogueCanvas/DialogueBox/CharacterImage" ).GetComponent<Image>();
            Model.Animator = GameObject.Find( "/DialogueCanvas/DialogueBox" ).GetComponent<Animator>();
            Model.Source = GetComponent<AudioSource>();

            controller = new DialogueManagerController(Model);
        }

        /// <summary>
        /// Checks if there is something in the model to display and if there was an input
        /// </summary>
        private void Update()
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

        /// <summary>
        /// Start new dialogue, and reset all data from previous dialogues
        /// </summary>
        private void StartDialogue()
        {
            controller.StartDialogue();
            DisplayNextSentence();
        }

        /// <summary>
        /// Display next sentence in dialogue
        /// </summary>
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