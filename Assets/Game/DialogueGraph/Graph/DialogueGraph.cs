using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

[System.Serializable]
public class DialogueGraph : GraphView
{
    public TextField FileName;

    public Dictionary<string, NodeGroup> Groups = new();
    public Dictionary<string, BaseNode> Nodes = new();

    private List<BaseNode> nodesToDelete = new();
    private List<NodeGroup> groupsToDelete = new ();

    private List<Port> compatiblePorts = new();

    public DialogueGraph()
    {
        FileName = new TextField() { value = "DialogueFileName", label = "File Name:" };

        AddGridBackground();
        AddStyle();

        graphViewChanged = OnGraphViewChanged; // присваиваем делегаты (можно и подписыватся, обычное событие)
        deleteSelection = OnDeleteSelection;

        elementsAddedToGroup = OnGroupElementAdded;
        elementsRemovedFromGroup = OnGroupElementRemoved;

        ChoiceNode.OnChoicePortRemoved = OnChoicePortRemoved; // костыль для удаления рёбер при удалении порта у ноды
    }

    private void AddGridBackground()
    {
        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();
    }

    private void AddStyle()
    {
        var graphStyle = (StyleSheet)EditorGUIUtility.Load($"{MyPaths.EDITOR}/GraphStyle.uss");
        var nodeStyle = (StyleSheet)EditorGUIUtility.Load($"{MyPaths.EDITOR}/NodeStyle.uss");
        styleSheets.Add(graphStyle);
        styleSheets.Add(nodeStyle);
    }


    #region Callbacks
    private GraphViewChange OnGraphViewChanged(GraphViewChange change)
    {
        UpdateEdges(change.edgesToCreate);
        OnElementsToRemove(change);
        return change;
    }

    private void OnDeleteSelection(string operationName, AskUser askUser)
    {
        CollectSelection();

        DeleteGroups();
        DeleteNodes();
    }


    private void OnGroupElementAdded(Group group, IEnumerable<GraphElement> elements) // группы сами регестрируют добавленные объекты оверрайд методом
    {
        var nodeGroup = (NodeGroup)group;

        foreach (var node in elements.OfType<BaseNode>())
        {
            node.GroupID = nodeGroup.ID;
        }
    }

    private void OnGroupElementRemoved(Group group, IEnumerable<GraphElement> elements)
    {
        foreach (var node in elements.OfType<BaseNode>())
        {
            node.GroupID = null;
        }
    }
    #endregion

    #region override
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter adapter) // подсвечивает порты нодов к которым можно присоединится
    {
        compatiblePorts.Clear();

        foreach (Port port in ports)
        {
            if (startPort == port) continue;
            if (startPort.node == port.node) continue;
            if (startPort.direction == port.direction) continue;
    
            compatiblePorts.Add(port);
        }

        return compatiblePorts;
    }
    #endregion

    #region OnGraphChanged
    private void UpdateEdges(IEnumerable<Edge> edges)
    {
        if (edges == null) return;

        foreach (var edge in edges)
        {
            if (edge.output.userData is ChoiceData choice && edge.input.node is BaseNode targetNode)
            {
                choice.NextNodeID = targetNode.ID;
            }
        }
    }

    private void OnElementsToRemove(GraphViewChange change)
    {
        if (change.elementsToRemove != null)
        {
            foreach (var element in change.elementsToRemove)
            {
                switch (element)
                {
                    case Edge edge: OnEdgeRemoved(edge); break;
                    case BaseNode node: Nodes.Remove(node.ID); break;
                    case NodeGroup group: Groups.Remove(group.ID); break;
                }
            }
        }
    }

    private void OnEdgeRemoved(Edge edge)
    {
        if (edge.output?.userData is ChoiceData choice)
        {
            choice.NextNodeID = string.Empty;
        }
    }
    #endregion

    #region OnElementDelete
    private void CollectSelection()
    {
        nodesToDelete.Clear();
        groupsToDelete.Clear();

        foreach (var element in selection)
        {
            switch (element)
            {
                case BaseNode node: nodesToDelete.Add(node); break;
                case NodeGroup group: groupsToDelete.Add(group); break;
            }
        }
    }

    private void DeleteGroups()
    {
        foreach (var group in groupsToDelete)
        {
            foreach (var node in group.containedElements.OfType<BaseNode>())
            {
                node.GroupID = null;
            }

            RemoveElement(group);
        }
    }

    private void DeleteNodes()
    {
        foreach (var node in nodesToDelete)
        {
            foreach (var port in node.inputContainer.Children().OfType<Port>())
                RemovePortEdges(port);
            
            foreach (var port in node.outputContainer.Children().OfType<Port>())
                RemovePortEdges(port);
            

            if (node.GroupID != null)
            {
                Groups[node.GroupID].RemoveElement(node);
            }

            RemoveElement(node);
        }
    }

    private void RemovePortEdges(Port port)
    {
        if (port?.connections == null) return;

        foreach (var edge in port.connections.ToList())
        {
            RemoveElement(edge);
        }
    }

    private void OnChoicePortRemoved(Port port)
    {
        if (port?.connections == null) return;

        foreach (var edge in port.connections)
        {
            if (edge.output?.userData is ChoiceData choice)
            {
                choice.NextNodeID = string.Empty;
            }
        }
    }
    #endregion

    #region Add Elements
    public void AddGroup(NodeGroup group)
    {
        AddElement(group);
        Groups[group.ID] = group;
    }

    public void AddNode(BaseNode node)
    {
        AddElement(node);
        Nodes[node.ID] = node;
    }
    #endregion

    public void ClearGraph()
    {
        foreach (var element in graphElements.ToList())
        {
            RemoveElement(element);
        }

        Nodes.Clear();
        Groups.Clear();
    }
}
