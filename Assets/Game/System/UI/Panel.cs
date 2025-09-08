using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Panel : MonoBehaviour
{
    public Action<bool> OnPanelToggle;

    [SerializeField] private MyButton[] buttons;

    public MyButton[] Buttons => buttons;
    public MyButton CurrentButton => buttons[CurrentButtonIndex];

    private Dictionary<MyButton, int> ButtonIndexes = new();
    public int CurrentButtonIndex { get; private set; }

    void Awake()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            ButtonIndexes.Add(buttons[i], i);
        }
    }


    public void SetActive(bool isTrue)
    {
        if (isTrue)
        {
            gameObject.SetActive(true); // подписка View не происходит если объект выключен, соотведственно не может включить анимацию отображения // костыль
            FocusFirstButton();
        }

        OnPanelToggle?.Invoke(isTrue);
    }

    public void ChoseButtonByIndex(int newButtonIndex)
    {
        if (!buttons[newButtonIndex].gameObject.activeInHierarchy)
        {
            FocusFirstButton(newButtonIndex);
            return;
        }

        CurrentButton.OnExit();
        CurrentButtonIndex = newButtonIndex;
        CurrentButton.OnEnter(); // вызывает срабатывание OnButtonChose но там проверка
    }

    public void FocusFirstButton(int startIndex = 0)
    {
        for (int i = startIndex; i < buttons.Length; i++)
        {
            if (buttons[i].gameObject.activeInHierarchy)
            {
                ChoseButtonByIndex(i);
                break;
            }
        }
    }


    private void OnButtonChose(MyButton newButton)
    {
        if (newButton == CurrentButton) return;

        CurrentButton.OnExit();
        CurrentButtonIndex = ButtonIndexes[newButton];
    }


    void OnEnable()
    {
        foreach (var button in buttons)
        {
            button.OnButtonEnter += OnButtonChose;
        }
    }

    void OnDisable()
    {
        foreach (var button in buttons)
        {
            button.OnButtonEnter -= OnButtonChose;
        }
    }
}
