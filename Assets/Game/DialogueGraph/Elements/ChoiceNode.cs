using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ChoiceNode : BaseNode
{
    public static Action<Port> OnChoicePortRemoved;

    public VisualElement choicesContainer;
    public List<ChoiceData> Choices = new();

    public override void Init()
    {
        AddChoicesContainer();
        AddChoiceButton();
        base.Init();
    }

    protected override void SetColor()
    {
        mainContainer.style.backgroundColor = new Color(0.200f, 0.200f, 0.100f);
    }

    protected override void SetOutput()
    {
        CreateChoicePort();

    }


    private void AddChoicesContainer()
    {
        choicesContainer = new VisualElement();
        choicesContainer.style.flexDirection = FlexDirection.Column;
        choicesContainer.style.marginTop = 4;

        extensionContainer.Add(choicesContainer);
    }

    private void AddChoiceButton()
    {
        var addChoiceButton = new Button(() => CreateChoicePort());
        addChoiceButton.text = "Add choice";

        addChoiceButton.AddToClassList("ds-node__button");
        mainContainer.Insert(1, addChoiceButton);
    }

    private void RemoveChoice(Port port, ChoiceData choiceData)
    {
        OnChoicePortRemoved?.Invoke(port); // нужно как то удалять элемент в гарфе

        Choices.Remove(choiceData);
        outputContainer.Remove(port);
    }


    private Port CreateChoicePort()
    {
        var choiceData = new ChoiceData("New Choice");
        var choiceElement = GetChoiseElement();
        var choiceText = GetChoiceField(choiceData);

        var choicePort = SetOutputPort();
        choicePort.userData = choiceData;

        var deleteButton = new Button(() => RemoveChoice(choicePort, choiceData));
        deleteButton.text = "X";
        deleteButton.AddToClassList("ds-node__button");

        Choices.Add(choiceData);

        choiceElement.Add(choiceText);
        choiceElement.Add(deleteButton);

        choicesContainer.Add(choiceElement);
        return choicePort;
    }

    private VisualElement GetChoiseElement()
    {
        var choiceElement = new VisualElement();
        choiceElement.style.flexDirection = FlexDirection.Row;
        choiceElement.style.alignItems = Align.Center;
        choiceElement.style.marginTop = 2;
        return choiceElement;
    }

    private TextField GetChoiceField(ChoiceData choiceData)
    {
        var choiceText = new TextField();
        choiceText.value = choiceData.Text;
        choiceText.multiline = true;

        choiceText.style.width = 240;
        choiceText.style.height = StyleKeyword.Auto;
        choiceText.style.minHeight = 40;
        choiceText.style.flexShrink = 0;

        choiceText.AddToClassList("ds-node__text-field");
        choiceText.AddToClassList("ds-node__text-field__hidden");

        choiceText.RegisterValueChangedCallback(callback => { choiceData.Text = callback.newValue; });
        return choiceText;
    }
}
