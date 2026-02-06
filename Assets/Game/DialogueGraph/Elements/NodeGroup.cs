using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

[System.Serializable]
public class NodeGroup : Group
{
    public string ID = Guid.NewGuid().ToString();
    public string Name => title;

    public List<BaseNode> Nodes = new();

    public NodeGroup(string newName = "Group")
    {
        title = newName;
    }


    protected override void OnElementsAdded(IEnumerable<GraphElement> elements)
    {
        foreach (var element in elements)
        {
            if (element is not BaseNode node) continue;
            Nodes.Add(node);
        }
    }

    protected override void OnElementsRemoved(IEnumerable<GraphElement> elements)
    {
        foreach (var element in elements)
        {
            if (element is not BaseNode node) continue;
            Nodes.Remove(node);
        }
    }
}
