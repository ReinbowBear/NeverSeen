using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueGraphSave
{
    private DialogueGraph graph;

    private string graphFileName => graph.FileName.value;
    private string folderPath;

    private List<BaseNode> graphNodes = new();
    private List<NodeGroup> graphGroups = new();

    private Dictionary<string, NodeGroup> loadedGroups = new();
    private Dictionary<string, BaseNode> loadedNodes = new();

    private DialogueGraphSO graphSO;
    private DialogueSO dialogueSO;

    public DialogueGraphSave(DialogueGraph graph, string fileName)
    {
        this.graph = graph;
        folderPath = $"{MyPaths.GRAPHS}/{fileName}";
    }


    public void Save()
    {
        CreateFolder(folderPath, "Global");
        CreateFolder(folderPath, "Groups");

        GetGraphElements();
        GetDialogueGraphSO();
        GetDialogueSO();

        SaveGroups();
        SaveNodes();

        SaveAsset(graphSO);
        SaveAsset(dialogueSO);
    }


    private void GetGraphElements()
    {
        graphNodes = graph.Nodes.Values.ToList();
        graphGroups = graph.Groups.Values.ToList();
    }

    private void GetDialogueGraphSO()
    {
        graphSO = CreateAsset<DialogueGraphSO>(folderPath, graphFileName); // SO для редактора графа
        graphSO.FileName = graphFileName;

        graphSO.Groups.Clear();
        graphSO.Nodes.Clear();
    }

    private void GetDialogueSO()
    {
        dialogueSO = CreateAsset<DialogueSO>(folderPath, $"{graphFileName}Container"); // SO для диалогов в геймплее
        dialogueSO.DialogueName = graphFileName;

        dialogueSO.Nodes.Clear();
    }

    #region SaveGroups
    private void SaveGroups()
    {
        foreach (NodeGroup group in graphGroups)
        {
            var groupData = GetNodeGroupData(group);
            graphSO.Groups.Add(groupData);
        }
    }

    private NodeGroupData GetNodeGroupData(NodeGroup group)
    {
        var groupData = new NodeGroupData
        {
            ID = group.ID,
            Name = group.title,
            Position = group.GetPosition().position,
        };
        return groupData;
    }
    #endregion

    #region SaveNodes
    private void SaveNodes()
    {
        foreach (BaseNode node in graphNodes)
        {
            var nodeData = GetNodeData(node);

            graphSO.Nodes.Add(nodeData);
            dialogueSO.Nodes.Add(nodeData);
        }
    }

    private DialogueNodeData GetNodeData(BaseNode node)
    {
        var nodeData = new DialogueNodeData()
        {
            ID = node.ID,
            GroupID = node.GroupID,
            NodeType = node.GetType().AssemblyQualifiedName,
            Position = node.GetPosition().position,

            Name = node.TitleField.value,
            Text = node.TextField.value,

            Choices = GetNodeChoices(node)
        };
        return nodeData;
    }

    private List<ChoiceData> GetNodeChoices(BaseNode node)
    {
        if (node is ChoiceNode choiceNode)
        {
            return choiceNode.Choices;
        }
        else
        {
            return null;
        }
    }
    #endregion

    #region Load
    public void Load()
    {
        graphSO = AssetDatabase.LoadAssetAtPath<DialogueGraphSO>($"{MyPaths.GRAPHS}/{graphFileName}/{graphFileName}.asset");

        if (graphSO == null)
        {
            EditorUtility.DisplayDialog
            (
                "God damn, file not found!",
                "The file path:\n\n" +
                $"\"{MyPaths.GRAPHS}/{graphFileName}/{graphFileName}\".\n\n" +
                $"Also only \"{typeof(DialogueGraphSO).Name}\" files are accepted, dumb bitch",
                "Fuck off"
            );

            return;
        }

        LoadGroups();
        LoadNodes();
        LoadNodesConnections();
    }

    private void LoadGroups() // ДУБЛИРУЕТ ФУНКЦИОНАЛ СОЗДАНИЯ ГРУП
    {
        foreach (NodeGroupData groupData in graphSO.Groups)
        {
            var group = new NodeGroup(groupData.Name);
            group.ID = groupData.ID;
            group.SetPosition(new Rect(groupData.Position, Vector2.zero));

            graph.AddGroup(group);

            loadedGroups.Add(group.ID, group);
        }
    }

    private void LoadNodes() // ДУБЛИРУЕТ ФУНКЦИОНАЛ СОЗДАНИЯ НОДОВ
    {
        foreach (DialogueNodeData nodeData in graphSO.Nodes)
        {
            var type = Type.GetType(nodeData.NodeType);
            var node = (BaseNode)Activator.CreateInstance(type);       

            node.ID = nodeData.ID;            
            node.TitleField.value = nodeData.Name;

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

    private void LoadNodesConnections()
    {
        foreach (var loadedNode in loadedNodes.Values)
        {
            foreach (var choicePort in loadedNode.outputContainer.Children())
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
                loadedNode.RefreshPorts();
            }
        }
    }
    #endregion

    #region Utility
    private void CreateFolder(string parentFolderPath, string newFolderName)
    {
        Directory.CreateDirectory($"{parentFolderPath}/{newFolderName}");
    }

    private T CreateAsset<T>(string path, string assetName) where T : ScriptableObject
    {
        string fullPath = $"{path}/{assetName}.asset";

        T asset = AssetDatabase.LoadAssetAtPath<T>(fullPath);

        if (asset == null)
        {
            asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, fullPath);
        }

        return asset;
    }

    private void SaveAsset(UnityEngine.Object asset)
    {
        EditorUtility.SetDirty(asset);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    #endregion
}
