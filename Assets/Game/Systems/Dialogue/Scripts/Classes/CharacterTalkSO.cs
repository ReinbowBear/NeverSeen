using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterTalkSO", menuName = "Scriptable Objects/CharacterTalkSO")]
public class CharacterTalkSO : ScriptableObject
{
    public string СharacterName;
    public List<Expression> Expressions;
}


[System.Serializable]
public struct Expression
{
    public ExpressionType expressionType;
    public Sprite Image;
    public AudioClip Voice;
}

public enum ExpressionType
{
    Default
}
