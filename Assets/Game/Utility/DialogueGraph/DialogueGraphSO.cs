using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueGraphSO : ScriptableObject
{
    public string FileName;
    public List<DialogueNodeData> Nodes = new ();
    public List<NodeGroupData> Groups = new ();

    public List<string> OldNodes;
    public List<string> OldGroups;
    public Dictionary<string, List<string>> OldGroupedNodes;
}


[System.Serializable]
public class DialogueNodeData
{
    public string ID;
    public string Name;
    public string Text;
    public List<ChoiceData> Choices;
    public string GroupID;
    public NodeType NodeType;
    public Vector2 Position;
}

[System.Serializable]
public class ChoiceData
{
    public string ID;
    public string Text;
}


[System.Serializable]
public class NodeGroupData
{
    public string ID;
    public string Name;
    public Vector2 Position;
}
