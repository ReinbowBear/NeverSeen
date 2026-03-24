using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GraphSave
{
    private List<BaseNode> graphNodes = new();
    private List<NodeGroup> graphGroups = new();

    private Dictionary<string, NodeGroup> loadedGroups = new();
    private Dictionary<string, BaseNode> loadedNodes = new();

    #region Save
    public void Save(DialogueGraph graph)
    {
        var graphName = graph.FileName.value;
        CollectGraphElements(graph);

        var graphData = new DialogueGraphData
        {
            FileName = graphName,
            Nodes = graphNodes.Select(GetNodeData).ToList(),
            Groups = graphGroups.Select(GetGroupData).ToList()
        };

        SaveLoad.Save(graphData, graphName, ConfigType.Graph);
    }


    private void CollectGraphElements(DialogueGraph graph)
    {
        graphNodes.Clear();
        graphGroups.Clear();

        graphNodes = graph.Nodes.Values.ToList();
        graphGroups = graph.Groups.Values.ToList();
    }


    private NodeGroupData GetGroupData(NodeGroup group)
    {
        return new NodeGroupData
        {
            ID = group.ID,
            Name = group.title,
            Position = group.GetPosition().position
        };
    }

    private DialogueNodeData GetNodeData(BaseNode node)
    {
        return new DialogueNodeData
        {
            ID = node.ID,
            GroupID = node.GroupID,
            NodeType = node.GetType().AssemblyQualifiedName,
            Position = node.GetPosition().position,

            Name = node.TitleField.value,
            Text = node.TextField.value,

            Choices = node is ChoiceNode choiceNode ? choiceNode.Choices : null
        };
    }
    #endregion

    #region Load
    public void Load(DialogueGraph graph, string fileName)
    {
        var graphData = SaveLoad.Load<DialogueGraphData>(fileName, ConfigType.Graph);
        if (graphData == null) return;

        LoadGroups(graph, graphData.Groups);
        LoadNodes(graph, graphData.Nodes);
        LoadNodesConnections(graph);
    }

    private void LoadGroups(DialogueGraph graph, List<NodeGroupData> groupsData) // ДУБЛИРУЕТ ФУНКЦИОНАЛ СОЗДАНИЯ ГРУП
    {
        loadedGroups.Clear();
        foreach (var groupData in groupsData)
        {
            var group = new NodeGroup(groupData.Name);
            group.ID = groupData.ID;
            group.SetPosition(new Rect(groupData.Position, Vector2.zero));

            graph.AddGroup(group);

            loadedGroups.Add(group.ID, group);
        }
    }

    private void LoadNodes(DialogueGraph graph, List<DialogueNodeData> nodesData) // ДУБЛИРУЕТ ФУНКЦИОНАЛ СОЗДАНИЯ НОДОВ
    {
        loadedNodes.Clear();
        foreach (var nodeData in nodesData)
        {
            var type = Type.GetType(nodeData.NodeType);
            var node = (BaseNode)Activator.CreateInstance(type);

            node.ID = nodeData.ID;
            node.TitleField.value = nodeData.Name;
            node.TextField.value = nodeData.Text;

            if (node is ChoiceNode choiceNode)
            {
                choiceNode.Choices = nodeData.Choices; // НЕ СОЗДАЕТ ПОРТЫ ПОД КОЛОВО ВАРИАНТОВ
            }

            node.SetPosition(new Rect(nodeData.Position, Vector2.zero));
            node.Init();
            node.Draw();

            graph.AddNode(node);
            loadedNodes.Add(node.ID, node);

            if (!string.IsNullOrEmpty(nodeData.GroupID))
            {
                var group = loadedGroups[nodeData.GroupID];
                node.GroupID = nodeData.GroupID;

                group.AddElement(node);
            }
        }
    }

    private void LoadNodesConnections(DialogueGraph graph)
    {
        foreach (var node in loadedNodes.Values)
        {
            foreach (var choicePort in node.outputContainer.Children())
            {
                var choiceData = (ChoiceData)choicePort.userData;
                if (string.IsNullOrEmpty(choiceData.NextNodeID)) continue;

                var nextNode = loadedNodes[choiceData.NextNodeID];
                var nextNodePort = (Port)nextNode.inputContainer.Children().First();

                var edge = new Edge
                {
                    output = (Port)choicePort,
                    input = nextNodePort
                };

                edge.output.Connect(edge);
                edge.input.Connect(edge);

                graph.AddElement(edge);
                node.RefreshPorts();
            }
        }
    }
    #endregion
}
