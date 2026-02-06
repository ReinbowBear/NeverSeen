using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueSO : ScriptableObject
{
    public string DialogueName;
    public List<DialogueNodeData> Nodes = new();
}
