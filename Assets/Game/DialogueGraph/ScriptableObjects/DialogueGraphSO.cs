using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueGraphSO : ScriptableObject
{
    public string FileName;
    public List<NodeGroupData> Groups = new();
    public List<DialogueNodeData> Nodes = new();
}

[System.Serializable]
public struct NodeGroupData
{
    public string ID;
    public string Name;
    public Vector2 Position;
}

[System.Serializable]
public struct DialogueNodeData
{
    public string ID;
    public string GroupID;
    public string NodeType;
    public Vector2 Position;

    public string Name;
    public string Text;

    public List<ChoiceData> Choices;
}

[System.Serializable]
public struct ChoiceData
{
    public string NextNodeID;
    public string Text;

    public ChoiceData(string text)
    {
        Text = text;
        NextNodeID = "";
    }
}
