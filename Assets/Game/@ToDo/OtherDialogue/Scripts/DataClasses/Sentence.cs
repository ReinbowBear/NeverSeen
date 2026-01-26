namespace DialogueManager.Models
{
    using UnityEngine;

    [System.Serializable]
    public class Sentence
    {
        public CharacterTalkSO Character;
        public int ExpressionIndex;

        [TextArea( 3, 10 )]
        public string SentenceText;
    }
}