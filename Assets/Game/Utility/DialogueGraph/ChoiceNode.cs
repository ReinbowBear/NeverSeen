using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ChoiceNode : DialogueNode
{
    public override void Init(DialogueGraph graph, Vector2 pos)
    {
        NodeType = NodeType.ChoiceNode;

        ChoiceData choiceData = new ChoiceData()
        {
            Text = $"Choice: {Choices.Count}"
        };

        Choices.Add(choiceData);

        AddChoiceButton();

        base.Init(graph, pos);
    }

    private void AddChoiceButton()
    {
        UnityEngine.UIElements.Button addChoiceButton = new UnityEngine.UIElements.Button(() => AddChoice())
        {
            text = "Add Choice"
        };

        addChoiceButton.AddToClassList("ds-node__button");

        mainContainer.Insert(1, addChoiceButton);
    }


    private Port CreateChoicePort(object userData)
    {
        Port choicePort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
        choicePort.portName = "";

        choicePort.userData = userData;
        ChoiceData choiceData = (ChoiceData)userData;

        UnityEngine.UIElements.Button deleteChoiceButton = new UnityEngine.UIElements.Button(() => DeleteChoise(choicePort, choiceData))
        {
            text = "X"
        };

        deleteChoiceButton.AddToClassList("ds-node__button");

        TextField choiceTextField = new TextField()
        {
            value = choiceData.Text
        };
        choiceTextField.RegisterValueChangedCallback(callback =>
        {
            choiceData.Text = callback.newValue;
        });

        choiceTextField.AddToClassList("ds-node__text-field");
        choiceTextField.AddToClassList("ds-node__text-field__hidden");
        choiceTextField.AddToClassList("ds-node__choice-text-field");

        choicePort.Add(choiceTextField);
        choicePort.Add(deleteChoiceButton);
        return choicePort;
    }


    private void AddChoice()
    {
        ChoiceData choiceData = new ChoiceData()
        {
            Text = "New Choice"
        };

        Choices.Add(choiceData);

        Port choicePort = CreateChoicePort(choiceData);

        outputContainer.Add(choicePort);
    }

    private void DeleteChoise(Port port, ChoiceData choiceData)
    {
        if (Choices.Count == 1) { return; }

        if (port.connected)
        {
            dialogueGraph.DeleteElements(port.connections);
        }

        Choices.Remove(choiceData);
        dialogueGraph.RemoveElement(port);
    }
}
