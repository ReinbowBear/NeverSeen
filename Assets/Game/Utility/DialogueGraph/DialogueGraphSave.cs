using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueGraphSave
{
    private DialogueGraph dialogueGraph;

    private string graphFileName;
    private string folderPath;

    private List<DialogueNode> graphNodes = new List<DialogueNode>();
    private List<NodeGroup> graphGroups = new List<NodeGroup>();

    private Dictionary<string, NodeGroupSO> createdGroupsSO = new Dictionary<string, NodeGroupSO>();
    private Dictionary<string, DialogueNodeSO> createdNodesSO = new Dictionary<string, DialogueNodeSO>();

    private Dictionary<string, DialogueNode> loadedNodes = new Dictionary<string, DialogueNode>();
    private Dictionary<string, NodeGroup> loadedGroups = new Dictionary<string, NodeGroup>();

    public DialogueGraphSave(DialogueGraph graph, string graphName)
    {
        dialogueGraph = graph;
        graphFileName = graphName;

        folderPath = $"{MyPaths.GRAPHS}/{graphFileName}";
    }


    public void Save()
    {
        CreateFolder(folderPath, "Global");
        CreateFolder(folderPath, "Groups");

        GetGraphElements();

        DialogueGraphSO graphData = CreateAsset<DialogueGraphSO>(folderPath, graphFileName);
        graphData.FileName = graphFileName;

        DialogueContainerSO dialogueContainer = CreateAsset<DialogueContainerSO>(folderPath, $"{graphFileName}Container");
        dialogueContainer.FileName = graphFileName;

        SaveGroups(graphData, dialogueContainer);
        SaveNodes(graphData, dialogueContainer);

        SaveAsset(graphData);
        SaveAsset(dialogueContainer);
    }


    private void GetGraphElements()
    {
        Type groupType = typeof(NodeGroup);

        dialogueGraph.graphElements.ForEach(graphElement =>
        {
            if (graphElement is DialogueNode node)
            {
                graphNodes.Add(node);
                return;
            }

            if (graphElement.GetType() == groupType)
            {
                NodeGroup group = (NodeGroup)graphElement;
                graphGroups.Add(group);
                return;
            }
        });
    }


    private void CreateFolder(string parentFolderPath, string newFolderName)
    {
        if (Directory.Exists($"{parentFolderPath}/{newFolderName}") == false)
        {
            Directory.CreateDirectory($"{parentFolderPath}/{newFolderName}");
        }
    }

    private void RemoveFolder(string path)
    {
        FileUtil.DeleteFileOrDirectory($"{path}.meta");
        FileUtil.DeleteFileOrDirectory($"{path}/");
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
        EditorUtility.SetDirty(asset); // помечает юнити ассеты как изменённые что бы тот их сохранил

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    #region SaveGroups
    private void SaveGroups(DialogueGraphSO graphData, DialogueContainerSO dialogueContainer)
    {
        List<string> toUpdateGroups = new List<string>();

        foreach (NodeGroup group in graphGroups)
        {
            SaveGroupToGraph(graphData, group);
            GroupToScriptableObject(dialogueContainer, group);

            toUpdateGroups.Add(group.title);
        }

        UpdateOldGroups(graphData, toUpdateGroups);
    }

    private void SaveGroupToGraph(DialogueGraphSO graphData, NodeGroup group)
    {
        NodeGroupData groupData = new NodeGroupData()
        {
            ID = group.ID,
            Name = group.title,
            Position = group.GetPosition().position
        };

        graphData.Groups.Add(groupData);
    }

    private void GroupToScriptableObject(DialogueContainerSO dialogueContainer, NodeGroup group)
    {
        string groupName = group.title;

        CreateFolder($"{folderPath}/Groups", groupName);

        NodeGroupSO dialogueGroup = CreateAsset<NodeGroupSO>($"{folderPath}/Groups/{groupName}", groupName);
        dialogueGroup.GroupName = groupName;

        createdGroupsSO.Add(group.ID, dialogueGroup);

        dialogueContainer.DialogueGroups.Add(dialogueGroup, new List<DialogueNodeSO>());
        SaveAsset(dialogueGroup);
    }
    #endregion

    #region SaveNodes
    private void SaveNodes(DialogueGraphSO graphData, DialogueContainerSO dialogueContainer)
    {
        SerializableDictionary<string, List<string>> groupedNodes = new SerializableDictionary<string, List<string>>();
        List<string> ungroupedNodes = new List<string>();

        foreach (DialogueNode node in graphNodes)
        {
            SaveNodeToGraph(graphData, node);
            NodeToScriptableObject(dialogueContainer, node);

            if (node.Group != null)
            {
                groupedNodes.AddItem(node.Group.title, node.NodeName);
                continue;
            }

            ungroupedNodes.Add(node.NodeName);
        }

        UpdateDialoguesChoicesConnections();

        UpdateOldGroupedNodes(graphData, groupedNodes);
        UpdateOldUngroupedNodes(graphData, ungroupedNodes);
    }

    private void SaveNodeToGraph(DialogueGraphSO graphData, DialogueNode node)
    {
        List<ChoiceData> choices = CloneNodeChoices(node.Choices); // благодаря клону листа, заполняем значения не ссылаясь на другие ноды, во избежание багов

        DialogueNodeData nodeData = new DialogueNodeData()
        {
            ID = node.ID,
            Name = node.NodeName,
            Text = node.NodeText,
            Choices = choices,
            GroupID = node.Group?.ID,
            NodeType = node.NodeType,
            Position = node.GetPosition().position
        };

        graphData.Nodes.Add(nodeData);
    }

    private void NodeToScriptableObject(DialogueContainerSO dialogueContainer, DialogueNode node)
    {
        DialogueNodeSO dialogue;

        if (node.Group != null)
        {
            dialogue = CreateAsset<DialogueNodeSO>($"{folderPath}/Groups/{node.Group.title}", node.NodeName);

            dialogueContainer.DialogueGroups.AddItem(createdGroupsSO[node.Group.ID], dialogue);
        }
        else
        {
            dialogue = CreateAsset<DialogueNodeSO>($"{folderPath}/Global", node.NodeName);

            dialogueContainer.UngroupedDialogues.Add(dialogue);
        }

        dialogue.Name = node.NodeName;
        dialogue.Text = node.NodeText;
        dialogue.Choices = ConverToDialogueChoices(node.Choices);
        dialogue.NodeType = node.NodeType;
        dialogue.IsStartNode = node.IsStartingNode();
        
        createdNodesSO.Add(node.ID, dialogue);

        SaveAsset(dialogue);
    }

    private List<DialogueNodeChoiceData> ConverToDialogueChoices(List<ChoiceData> nodeChoices) // какой то костыль связанный с классами как я понял
    {
        List<DialogueNodeChoiceData> dialogueChoices = new List<DialogueNodeChoiceData>();

        foreach (ChoiceData nodeChoice in nodeChoices)
        {
            DialogueNodeChoiceData choiceData = new DialogueNodeChoiceData()
            {
                Text = nodeChoice.Text
            };

            dialogueChoices.Add(choiceData);
        }

        return dialogueChoices;
    }
    #endregion

    #region Update
    private void UpdateOldGroups(DialogueGraphSO graphData, List<string> currentGroups)
    {
        if (graphData.OldGroups != null && graphData.OldGroups.Count != 0)
        {
            List<string> groupsToRemove = graphData.OldGroups.Except(currentGroups).ToList();

            foreach (string group in groupsToRemove)
            {
                RemoveFolder($"{folderPath}/{graphFileName}/Groups/{group}");
            }
        }

        graphData.OldGroups = new List<string>(currentGroups);
    }

    private void UpdateOldUngroupedNodes(DialogueGraphSO graphData, List<string> currentUngroupedNodes)
    {
        if (graphData.OldNodes != null && graphData.OldNodes.Count != 0)
        {
            List<string> nodesToRemove = graphData.OldNodes.Except(currentUngroupedNodes).ToList();

            foreach (string nodeToRemove in nodesToRemove)
            {
                AssetDatabase.DeleteAsset($"{folderPath}/{graphFileName}/Global/{nodeToRemove}.asset");
            }
        }

        graphData.OldNodes = new List<string>(currentUngroupedNodes);
    }

    private void UpdateOldGroupedNodes(DialogueGraphSO graphData, SerializableDictionary<string, List<string>> currentGroupedNodes)
    {
        if (graphData.OldGroupedNodes != null && graphData.OldGroupedNodes.Count != 0)
        {
            foreach (KeyValuePair<string, List<string>> oldGroupedNode in graphData.OldGroupedNodes)
            {
                List<string> nodesToRemove = new List<string>();

                if (currentGroupedNodes.ContainsKey(oldGroupedNode.Key))
                {
                    nodesToRemove = oldGroupedNode.Value.Except(currentGroupedNodes[oldGroupedNode.Key]).ToList();
                }

                foreach (string nodeToRemove in nodesToRemove)
                {
                    AssetDatabase.DeleteAsset($"{folderPath}/{graphFileName}/Groups/{oldGroupedNode}.asset");
                }
            }
        }

        graphData.OldGroupedNodes = new SerializableDictionary<string, List<string>>(currentGroupedNodes);
    }

    private void UpdateDialoguesChoicesConnections()
    {
        foreach (DialogueNode node in graphNodes)
        {
            DialogueNodeSO dialogue = createdNodesSO[node.ID];

            for (int choiceIndex = 0; choiceIndex < node.Choices.Count; choiceIndex++)
            {
                ChoiceData nodeChoice = node.Choices[choiceIndex];

                if (string.IsNullOrEmpty(nodeChoice.ID))
                {
                    continue;
                }

                dialogue.Choices[choiceIndex].NextDialogue = createdNodesSO[nodeChoice.ID];
                SaveAsset(dialogue);
            }
        }
    }
    #endregion

    #region Load
    public void Load()
    {
        DialogueGraphSO graphData = AssetDatabase.LoadAssetAtPath<DialogueGraphSO>($"{MyPaths.GRAPHS}/{graphFileName}/{graphFileName}.asset");

        if (graphData == null)
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

        dialogueGraph.Name = graphData.FileName;

        LoadGroups(graphData.Groups);
        LoadNodes(graphData.Nodes);
        LoadNodesConnections();
    }

    private void LoadGroups(List<NodeGroupData> groups)
    {
        foreach (NodeGroupData groupData in groups)
        {
            NodeGroup group = dialogueGraph.CreateGroup(groupData.Name, groupData.Position);
            group.ID = groupData.ID;

            loadedGroups.Add(group.ID, group);
        }
    }

    private void LoadNodes(List<DialogueNodeData> nodes)
    {
        foreach (DialogueNodeData nodeData in nodes)
        {
            List<ChoiceData> choices = CloneNodeChoices(nodeData.Choices);

            DialogueNode node = dialogueGraph.CreateNode(nodeData.NodeType, nodeData.Position, false, nodeData.Name);

            node.ID = nodeData.ID;            
            //node.NodeName = nodeData.Name;
            node.Choices = choices;
            node.NodeText = nodeData.Text;
            node.Draw();

            dialogueGraph.AddElement(node);

            loadedNodes.Add(node.ID, node);

            if (string.IsNullOrEmpty(nodeData.GroupID))
            {
                continue;
            }

            NodeGroup group = loadedGroups[nodeData.GroupID];
            node.Group = group;

            group.AddElement(node);
        }
    }

    private void LoadNodesConnections()
    {
        foreach (KeyValuePair<string, DialogueNode> loadedNode in loadedNodes)
        {
            foreach (Port choicePort in loadedNode.Value.outputContainer.Children())
            {
                ChoiceData choiceData = (ChoiceData) choicePort.userData;

                if (string.IsNullOrEmpty(choiceData.ID))
                {
                    continue;
                }

                DialogueNode nextNode = loadedNodes[choiceData.ID];

                Port nextNodeInputPort = (Port) nextNode.inputContainer.Children().First();

                Edge edge = choicePort.ConnectTo(nextNodeInputPort);

                dialogueGraph.AddElement(edge);

                loadedNode.Value.RefreshPorts();
            }
        }
    }
    #endregion

    #region Utility
    private List<ChoiceData> CloneNodeChoices(List<ChoiceData> nodeChoices)
    {
        List<ChoiceData> choices = new List<ChoiceData>();

        foreach (ChoiceData choice in nodeChoices)
        {
            ChoiceData choiceData = new ChoiceData()
            {
                Text = choice.Text,
                ID = choice.ID
            };

            choices.Add(choiceData);
        }

        return choices;
    }
}

public static class CollectionUtility
{
    public static void AddItem<K, V>(this SerializableDictionary<K, List<V>> serializableDictionary, K key, V value) // не уверен зачем и что делает этот метод, но он решал какуе то проблему
    {
        if (serializableDictionary.ContainsKey(key))
        {
            serializableDictionary[key].Add(value);
            return;
        }

        serializableDictionary.Add(key, new List<V>() { value });
    }
}
#endregion