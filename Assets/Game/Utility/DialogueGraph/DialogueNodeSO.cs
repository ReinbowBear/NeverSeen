using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNodeSO : ScriptableObject
{
    public string Name;
    [field: TextArea()] public string Text;
    public List<DialogueNodeChoiceData> Choices;
    public NodeType NodeType;
    public bool IsStartNode;
}

[System.Serializable]
public class DialogueNodeChoiceData // весь этот дибильный класс существует ради одной функции!
{
    public string Text;
    public DialogueNodeSO NextDialogue;
}
