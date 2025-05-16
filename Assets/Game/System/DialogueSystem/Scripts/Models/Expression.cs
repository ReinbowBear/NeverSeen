namespace DialogueManager.Models
{
    using UnityEngine;

    [System.Serializable]
    public class Expression
    {
        [Header("Expression")]
        public string Name;
        public Sprite Image;

        public Expression(string newName, Sprite newImage)
        {
            Image = newImage;
            Name = newName;
        }

        public Expression()
        {
            Name = string.Empty;
        }
    }
}