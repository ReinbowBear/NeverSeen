using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraph : GraphView
{
    public string Name = "DialogueFileName";

    private SerializableDictionary<string, NodeErrorData> ungroupedNodes = new SerializableDictionary<string, NodeErrorData>(); // кастомный класс SerializableDictionary, может быть заменён на обычный Dictionary
    private SerializableDictionary<Group, SerializableDictionary<string, NodeErrorData>> groupedNodes = new SerializableDictionary<Group, SerializableDictionary<string, NodeErrorData>>(); // отдельный список для груп нодов, с ним связанно много кода а всё что он делает, позволяет групирировать ноды в отдельные контейнеры, надо ли
    private SerializableDictionary<string, GroupErrorData> groups = new SerializableDictionary<string, GroupErrorData>();

    public DialogueGraph()
    {
        AddStyle();
        AddGridBackground();

        OnGraphViewChanged(); // override методы
        OnElementDelete();
        OnGroupElementAdded();
        OnGroupElementDelete();
        OnGroupRenamed();

        AddManipulators();
    }

    private void AddStyle()
    {
        StyleSheet graphStyle = (StyleSheet)EditorGUIUtility.Load($"{MyPaths.EDITOR}/GraphStyle.uss");
        StyleSheet nodeStyle = (StyleSheet)EditorGUIUtility.Load($"{MyPaths.EDITOR}/NodeStyle.uss");
        styleSheets.Add(graphStyle);
        styleSheets.Add(nodeStyle);
    }

    private void AddGridBackground()
    {
        GridBackground grid = new GridBackground();
        Insert(0, grid);
    }

    #region Override
    private void OnGraphViewChanged() // устанавливаем ID связям
    {
        graphViewChanged = (changes) =>
        {
            if (changes.edgesToCreate != null)
            {
                foreach (Edge edge in changes.edgesToCreate)
                {
                    DialogueNode nextNode = (DialogueNode) edge.input.node;
                    ChoiceSave choiceData = (ChoiceSave) edge.output.userData;

                    choiceData.ID = nextNode.ID;
                }
            }

            if (changes.elementsToRemove != null)
            {
                Type edgeType = typeof(Edge);

                foreach (GraphElement element in changes.elementsToRemove)
                {
                    if (element.GetType() != edgeType)
                    {
                        continue;
                    }

                    Edge edge = (Edge) element;
                    ChoiceSave choiceData = (ChoiceSave) edge.output.userData;
                    choiceData.ID = "";
                }
            }

            return changes;
        };
    }

    private void OnElementDelete()
    {
        deleteSelection = (operationName, askUser) =>
        {
            Type groupType = typeof(NodeGroup);
            Type edgeType = typeof(Edge);

            List<DialogueNode> nodesToDelete = new List<DialogueNode>();
            List<Edge> edgesToDelete = new List<Edge>();
            List<NodeGroup> groupsToDelete = new List<NodeGroup>();

            foreach (GraphElement element in selection)
            {
                if (element is DialogueNode node)
                {
                    nodesToDelete.Add(node);
                    continue;
                }

                if (element.GetType() == edgeType)
                {
                    edgesToDelete.Add((Edge)element);
                    continue;
                }

                if (element.GetType() != groupType) // я не уверен почему тут GetType вместо простого is но так было в туторе
                {
                    continue;
                }

                groupsToDelete.Add((NodeGroup)element);
            }

            foreach (NodeGroup group in groupsToDelete) // групы должны удалятся первыми
            {
                List<DialogueNode> groupNodes = new List<DialogueNode>(); // очищаем ноды с групы (переводим в лист несгрупирированных)
                foreach (GraphElement graphElement in group.containedElements)
                {
                    if ((graphElement is DialogueNode) == false)
                    {
                        continue;
                    }

                    groupNodes.Add((DialogueNode)graphElement);
                }

                group.RemoveElements(groupNodes);
                RemoveElement(group);
            }

            DeleteElements(edgesToDelete);

            foreach (DialogueNode node in nodesToDelete)
            {
                if (node.Group != null)
                {
                    node.Group.RemoveElement(node);
                }

                RemoveNode(node);
                node.DisconnectPorts();
                RemoveElement(node);
            }
        };
    }

    private void OnGroupElementAdded()
    {
        elementsAddedToGroup = (group, elements) =>
        {
            foreach (GraphElement element in elements)
            {
                if ((element is DialogueNode) == false)
                {
                    continue;
                }

                NodeGroup nodeGroup = (NodeGroup)group;
                DialogueNode node = (DialogueNode)element;

                RemoveNode(node);
                AddGoupedNode(node, nodeGroup);
            }
        };
    }

    private void OnGroupElementDelete()
    {
        elementsRemovedFromGroup = (group, elements) =>
        {
            foreach (GraphElement element in elements)
            {
                if ((element is DialogueNode) == false)
                {
                    continue;
                }

                DialogueNode node = (DialogueNode)element;
                RemoveGroupedNode(node, group);
                AddNode(node);
            }
        };
    }

    private void OnGroupRenamed()
    {
        groupTitleChanged = (group, newTitle) =>
        {
            NodeGroup nodeGroup = (NodeGroup)group;
            RemoveGroup(nodeGroup);
            nodeGroup.oldTitle = newTitle;
            AddGroup(nodeGroup);
        };
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter adapter) // подсвечивает порты нодов к которым можно присоединится (вроде как)
    {
        List<Port> compatiblePorts = new List<Port>();

        ports.ForEach(port =>
        {
            if (startPort == port || startPort.node == port.node || startPort.direction == port.direction)
            {
                return;
            }

            compatiblePorts.Add(port);
        });

        return compatiblePorts;
    }
    #endregion

    #region Add and Remove
    public void AddNode(DialogueNode node)
    {
        string nodeName = node.NodeName;

        if (ungroupedNodes.ContainsKey(nodeName) == false)
        {
            NodeErrorData nodeError = new NodeErrorData();
            nodeError.Nodes.Add(node);

            ungroupedNodes.Add(nodeName, nodeError);
            return;
        }

        ungroupedNodes[nodeName].Nodes.Add(node);

        Color errorColor = ungroupedNodes[nodeName].Color;
        node.SetErrorColor(errorColor);

        if (ungroupedNodes[nodeName].Nodes.Count == 2) // решает баг с тем что не подсвечивается первая нода
        {
            ungroupedNodes[nodeName].Nodes[0].SetErrorColor(errorColor);
        }
    }

    public void AddGoupedNode(DialogueNode node, NodeGroup group)
    {
        node.Group = group;
        string nodeName = node.NodeName;

        if (groupedNodes.ContainsKey(group) == false)
        {
            groupedNodes.Add(group, new SerializableDictionary<string, NodeErrorData>());
        }

        if (groupedNodes[group].ContainsKey(nodeName) == false)
        {
            NodeErrorData nodeError = new NodeErrorData();
            nodeError.Nodes.Add(node);

            groupedNodes[group].Add(nodeName, nodeError);
            return;
        }

        groupedNodes[group][nodeName].Nodes.Add(node);

        Color errorColor = groupedNodes[group][nodeName].Color;
        node.SetErrorColor(errorColor);

        if (groupedNodes[group][nodeName].Nodes.Count == 2)
        {
            groupedNodes[group][nodeName].Nodes[0].SetErrorColor(errorColor);
        }
    }

    private void AddGroup(NodeGroup group)
    {
        string groupName = group.title;

        if (groups.ContainsKey(groupName) == false)
        {
            GroupErrorData groupError = new GroupErrorData();
            groupError.Groups.Add(group);

            groups.Add(groupName, groupError);
            return;
        }

        groups[groupName].Groups.Add(group);

        Color errorColor = groups[groupName].Color;
        group.SetErrorColor(errorColor);

        if (groups[groupName].Groups.Count == 2)
        {
            groups[groupName].Groups[0].SetErrorColor(errorColor);
        }
    }


    public void RemoveNode(DialogueNode node)
    {
        List<DialogueNode> nodes = ungroupedNodes[node.NodeName].Nodes;

        nodes.Remove(node);
        node.SetDefaultColor();

        if (nodes.Count == 1)
        {
            nodes[0].SetDefaultColor();
            return;
        }

        if (nodes.Count == 0)
        {
            ungroupedNodes.Remove(node.NodeName);
        }
    }

    public void RemoveGroupedNode(DialogueNode node, Group group)
    {
        node.Group = null;
        List<DialogueNode> nodes = groupedNodes[group][node.NodeName].Nodes;

        nodes.Remove(node);
        node.SetDefaultColor();

        if (nodes.Count == 1)
        {
            nodes[0].SetDefaultColor();
            return;
        }

        if (nodes.Count == 0)
        {
            groupedNodes[group].Remove(node.NodeName);

            if (groupedNodes[group].Count == 0)
            {
                groupedNodes.Remove(group);
            }
        }
    }

    private void RemoveGroup(NodeGroup group)
    {
        List<NodeGroup> groupsList = groups[group.oldTitle].Groups;

        groupsList.Remove(group);
        group.SetDefaultColor();

        if (groupsList.Count == 1)
        {
            groupsList[0].SetDefaultColor();
            return;
        }

        if (groupsList.Count == 0)
        {
            ungroupedNodes.Remove(group.oldTitle);
        }
    }
    #endregion

    #region Manipulators
    private void AddManipulators() // добавляем функции созданному окну
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale); // возможность зумить
        this.AddManipulator(new ContentDragger()); // перетягивание холста
        this.AddManipulator(new SelectionDragger()); // перетягивание выбранного
        this.AddManipulator(new RectangleSelector()); // селект бокс при зажатии мышки

        this.AddManipulator(CreateNodeMenu(NodeType.TextNode, "Add Text Node"));
        this.AddManipulator(CreateNodeMenu(NodeType.ChoiceNode, "Add Choice Node"));

        this.AddManipulator(CreateGroupMenu());
    }


    private IManipulator CreateNodeMenu(NodeType type, string actionTitle)
    {
        ContextualMenuManipulator contextualMenu = new ContextualMenuManipulator
        (menuEvent => menuEvent.menu.AppendAction(actionTitle, actionEvent => AddElement(CreateNode(type, GetCorrectPos(actionEvent.eventInfo.localMousePosition)))));

        return contextualMenu;
    }

    public DialogueNode CreateNode(NodeType type, Vector2 position, bool willDraw = true)
    {
        Type nodeType = Type.GetType(type.ToString());
        DialogueNode node = (DialogueNode) Activator.CreateInstance(nodeType);

        node.Init(this, position);

        if (willDraw) // нужно в скрипте загрузки графа
        {
            node.Draw();
        }

        AddNode(node);
        AddElement(node);
        return node;
    }


    private IManipulator CreateGroupMenu()
    {
        ContextualMenuManipulator contextualMenu = new ContextualMenuManipulator
        (menuEvent => menuEvent.menu.AppendAction("Add Group", actionEvent => CreateGroup("Dialogue group", GetCorrectPos(actionEvent.eventInfo.localMousePosition))));

        return contextualMenu;
    }

    public NodeGroup CreateGroup(string titleName, Vector2 pos)
    {
        NodeGroup group = new NodeGroup(titleName, pos);

        AddGroup(group);
        AddElement(group);

        foreach (GraphElement selectedElement in selection)
        {
            if ((selectedElement is NodeGroup) == false)
            {
                continue;
            }

            group.AddElement((DialogueNode) selectedElement);
        }

        return group;
    }
    #endregion

    #region Utility
    private Vector2 GetCorrectPos(Vector2 mousePos) // устраняет баг где позиция мыши не учитывает маштаб графа
    {
        Vector2 correctPos = contentViewContainer.WorldToLocal(mousePos);
        return correctPos;
    }

    public void ClearGraph()
    {
        graphElements.ForEach(element => RemoveElement(element));

        ungroupedNodes.Clear();
        groupedNodes.Clear();
        groups.Clear();
    }
    #endregion
}
