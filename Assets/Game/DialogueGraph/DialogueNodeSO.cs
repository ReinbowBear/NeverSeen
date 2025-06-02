using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNodeSO : ScriptableObject
{
    public string DialogueName;
    [field: TextArea()] public string Text;
    public List<DialogueNodeChoiceData> Choices;
    public NodeType NodeType;
    public bool IsStartNode;
    public void Init(string dialogueName, string text, List<DialogueNodeChoiceData> choices, NodeType nodeType, bool isStartingDialogue)
    {
        DialogueName = dialogueName;
        Text = text;
        Choices = choices;
        NodeType = nodeType;
        IsStartNode = isStartingDialogue;
    }
}

[System.Serializable]
public class DialogueNodeChoiceData
{
    public string Text;
    public DialogueNodeSO NextDialogue;
}
