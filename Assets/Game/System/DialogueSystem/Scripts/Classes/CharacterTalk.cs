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
