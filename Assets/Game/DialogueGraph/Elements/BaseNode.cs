using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public abstract class BaseNode : Node
{
    public string ID = Guid.NewGuid().ToString();
    public string GroupID;

    public TextField TitleField = new();
    public TextField TextField = new();

    public virtual void Init()
    {
        mainContainer.AddToClassList("ds-node__main-container");
        extensionContainer.AddToClassList("ds-node__extension-container");
        SetColor();
    }

    protected abstract void SetColor();

    public void Draw() // методы связанные с настройкой разных областей контейнера
    {
        SetTitle();
        SetInputPort();
        SetOutput();
        SetExtension();
    }


    protected void SetTitle()
    {
        TitleField.value = $"{GetType()}";
        TitleField.AddToClassList("ds-node__textfield");
        TitleField.AddToClassList("ds-node__filename-textfield");
        TitleField.AddToClassList("ds-node__textfield__hidden");
        titleContainer.Insert(0, TitleField);
    }

    protected void SetInputPort()
    {
        var inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        inputPort.portName = "Input";
        inputContainer.Add(inputPort);
    }

    protected abstract void SetOutput();
    protected Port SetOutputPort()
    {
        var port = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
        port.portName = "Next";

        outputContainer.Add(port);
        return port;
    }

    protected void SetExtension()
    {
        var  dataContainer = new VisualElement();
        var  foldout = new Foldout() { text = "Dialogue text" };

        TextField.multiline = true;
        TextField.style.whiteSpace = WhiteSpace.Normal;

        dataContainer.AddToClassList("ds-node__custom-data-container");
        TextField.AddToClassList("ds-node__textfield");
        TextField.AddToClassList("ds-node__quote-textfield");

        foldout.Add(TextField);
        dataContainer.Add(foldout);
        extensionContainer.Add(dataContainer);

        RefreshExpandedState();
    }
}

[System.Serializable]
public class NodeData
{
    public string Id;
    public string GroupId;
    public string NodeType; // "TextNode" или "ChoiceNode"
    public string Text;     // текст ноды

    public List<ChoiceData> Choices; // для ChoiceNode, для TextNode будет null или пустой
    public Vector2 Position;
}

