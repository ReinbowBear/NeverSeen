using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphManipulators
{
    private DialogueGraph graph;
    private ContextualMenuManipulator contextMenu;

    public DialogueGraphManipulators(DialogueGraph graph)
    {
        this.graph = graph;
        AddManipulators();
    }


    private void AddManipulators()
    {
        graph.SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale); // возможность зумить

        graph.AddManipulator(new ContentDragger()); // перетягивание холста
        graph.AddManipulator(new SelectionDragger()); // перетягивание выбранного
        graph.AddManipulator(new RectangleSelector()); // селект бокс при зажатии мышки

        contextMenu = new ContextualMenuManipulator(AddMyManipulators);
        graph.AddManipulator(contextMenu);
    }

    private void AddMyManipulators(ContextualMenuPopulateEvent contextMenu)
    {
        contextMenu.menu.AppendAction("Add/Text Node", MenuAction => CreateNode(typeof(TextNode), GetMousePos(MenuAction)));
        contextMenu.menu.AppendAction("Add/Choice Node", MenuAction => CreateNode(typeof(ChoiceNode), GetMousePos(MenuAction)));
        contextMenu.menu.AppendAction("Add Group", MenuAction => CreateGroup(GetMousePos(MenuAction)));
    }

    #region MyManipulators
    public BaseNode CreateNode(Type nodeType, Vector2 position)
    {
        var node = (BaseNode)Activator.CreateInstance(nodeType);
        node.SetPosition(new Rect(position, Vector2.zero));
        node.Init();
        node.Draw();

        graph.AddNode(node);
        return node;
    }


    public NodeGroup CreateGroup(Vector2 position)
    {
        var group = new NodeGroup();
        group.SetPosition(new Rect(position, Vector2.zero));

        graph.AddGroup(group);

        foreach (GraphElement selectedElement in graph.selection) // добавляем в группу выделенное если есть
        {
            if (selectedElement is not BaseNode node) continue;
            group.AddElement(node);
        }

        return group;
    }
    #endregion


    private Vector2 GetMousePos(DropdownMenuAction MenuAction) // учитываем масштаб графа
    {
        return graph.contentViewContainer.WorldToLocal(MenuAction.eventInfo.mousePosition);
    }
}
