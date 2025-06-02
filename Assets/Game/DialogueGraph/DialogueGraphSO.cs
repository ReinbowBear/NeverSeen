using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueGraphSO : ScriptableObject
{
    public string FileName;
    public List<DialogueNodeSave> Nodes = new List<DialogueNodeSave>();
    public List<NodeGroupSave> Groups = new List<NodeGroupSave>();

    public List<string> OldNodes;
    public List<string> OldGroups;
    public SerializableDictionary<string, List<string>> OldGroupedNodes;
}


[System.Serializable]
public class DialogueNodeSave
{
    public string ID;
    public string Name;
    public string Text;
    public List<ChoiceSave> Choices;
    public string GroupID;
    public NodeType NodeType;
    public Vector2 Position;
}

[System.Serializable]
public class ChoiceSave
{
    public string ID;
    public string Text;
}


[System.Serializable]
public class NodeGroupSave
{
    public string ID;
    public string Name;
    public Vector2 Position;
}
