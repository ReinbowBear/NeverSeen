using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueNode : Node
{
    public string ID;
    public string NodeName = "Dialogue Node";
    public string NodeText = "your text";

    public List<ChoiceSave> Choices = new List<ChoiceSave>();
    public NodeType NodeType;
    private Color defaultColor = new Color(29f / 255f, 29f / 255f, 30f / 255f); // деление на 255 связано с какой то конвертацией и Color32
    protected DialogueGraph dialogueGraph;
    public NodeGroup Group;

    public virtual void Init(DialogueGraph graph, Vector2 position)
    {
        ID = Guid.NewGuid().ToString(); // какое то автоматическое айди

        dialogueGraph = graph;

        mainContainer.AddToClassList("ds-node__main-container");
        extensionContainer.AddToClassList("ds-node__extension-container"); // как то связанно с NodeStyle.uss похоже на пометку

        SetPosition(new Rect(position, Vector2.zero));
    }

    public void Draw() // методы связанные с настройкой разных областей контейнера
    {
        SetTitle();
        SetInput();
        SetOutput();
        SetExtension();
    }


    protected virtual void SetTitle()
    {
        TextField nodeName = CreateTextField(NodeName, callBack =>
        {
            if (Group == null) // дефолтный перерасчёт если у нода нет групы 
            {
                dialogueGraph.RemoveNode(this);
                NodeName = callBack.newValue;
                dialogueGraph.AddNode(this);
                return;
            }

            NodeGroup currentGroup = Group;

            dialogueGraph.RemoveGroupedNode(this, Group);
            NodeName = callBack.newValue;
            dialogueGraph.AddGoupedNode(this, currentGroup);
        });

        nodeName.AddToClassList("ds-node__textfield");
        nodeName.AddToClassList("ds-node__filename-textfield");
        nodeName.AddToClassList("ds-node__textfield__hidden");

        titleContainer.Insert(0, nodeName);
    }

    protected virtual void SetInput()
    {
        Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        inputPort.portName = "Input";

        inputContainer.Add(inputPort);
    }

    protected virtual void SetOutput()
    {
        foreach (var choice in Choices)
        {
            Port outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            outputPort.portName = "";
            outputPort.userData = choice; // кажется в userData можно хранить любые данные типа

            TextField ChoiceTextField = new TextField() { value = choice.Text };

            ChoiceTextField.AddToClassList("ds-node__textfield");
            ChoiceTextField.AddToClassList("ds-node__choice-textfield");
            ChoiceTextField.AddToClassList("ds-node__textfield__hidden");

            outputPort.Add(ChoiceTextField);
            outputContainer.Add(outputPort);
        }
    }

    protected virtual void SetExtension()
    {
        VisualElement dataContainer = new VisualElement();
        dataContainer.AddToClassList("ds-node__custom-data-container");

        Foldout foldout = new Foldout()
        {
            text = "Dialogue text"
        };

        TextField textField = CreateTextField(NodeText, callback => { NodeText = callback.newValue; }); // нужно через callback менять значение переменной для сохранений
        textField.multiline = true;

        textField.AddToClassList("ds-node__textfield");
        textField.AddToClassList("ds-node__quote-textfield");

        foldout.Add(textField);
        dataContainer.Add(foldout);

        extensionContainer.Add(dataContainer);
        RefreshExpandedState();
    }


    protected TextField CreateTextField(string newValue = null, EventCallback<ChangeEvent<string>> onValueChanged = null) // эта функция существует только для колбеков, что бы перерасчитывать ноду при изменении имени
    {
        TextField textField = new TextField()
        {
            value = newValue
        };

        if (onValueChanged != null)
        {
            textField.RegisterValueChangedCallback(onValueChanged);
        }

        return textField;
    }

    public bool IsStartingNode()
    {
        Port inputPort = (Port)inputContainer.Children().First();
        return !inputPort.connected;
    }

    public void DisconnectPorts()
    {
        foreach (Port port in inputContainer.Children())
        {
            if (port.connected == false) { continue; }

            dialogueGraph.DeleteElements(port.connections);
        }

        foreach (Port port in outputContainer.Children())
        {
            if (port.connected == false) { continue; }

            dialogueGraph.DeleteElements(port.connections);
        }
    }


    public void SetDefaultColor()
    {
        mainContainer.style.backgroundColor = defaultColor;
    }

    public void SetErrorColor(Color color)
    {
        mainContainer.style.backgroundColor = color;
    }
}

public enum NodeType // использует Activator.CreateInstance(nodeType); в DialogueGraph
{
    TextNode,
    ChoiceNode
}