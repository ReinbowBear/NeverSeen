using UnityEngine;
using DialogueManager.Models;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterTalk : ScriptableObject
{
    public string characterName;
    public List<Expression> Expressions;
    public AudioClip Voice;
}

namespace DialogueManager.Models
{
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