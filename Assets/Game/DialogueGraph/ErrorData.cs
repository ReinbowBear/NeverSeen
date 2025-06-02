using System.Collections.Generic;
using UnityEngine;

public class NodeErrorData
{
    public List<DialogueNode> Nodes = new List<DialogueNode>();
    public Color Color;

    public NodeErrorData()
    {
        SetRandomColor();
    }


    private void SetRandomColor()
    {
        Color = new Color32((byte)Random.Range(65, 256), (byte)Random.Range(50, 176), (byte)Random.Range(50, 176), 255);
    }
}

public class GroupErrorData
{
    public List<NodeGroup> Groups = new List<NodeGroup>();
    public Color Color;

    public GroupErrorData()
    {
        SetRandomColor();
    }


    private void SetRandomColor()
    {
        Color = new Color32((byte)Random.Range(65, 256), (byte)Random.Range(50, 176), (byte)Random.Range(50, 176), 255);
    }
}
