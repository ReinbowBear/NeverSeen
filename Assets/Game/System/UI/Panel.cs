using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public Button[] Buttons;
    public Button CurrentButton => Buttons[CurrentButtonIndex];
    public int CurrentButtonIndex { get; private set; }

    public void NavigateInput(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();

        int direction = (input == 1) ? -1 : 1; // смотрим направление ввода
        int newButtonIndex = (CurrentButtonIndex + direction + Buttons.Length) % Buttons.Length; // если вышли за край, возращаемся с другой стороны

        ChoseButtonByIndex(newButtonIndex);
    }


    public void ChoseButtonByIndex(int index)
    {
        for (int i = index; i < Buttons.Length; i++)
        {
            if (Buttons[i].gameObject.activeInHierarchy)
            {
                ChoseButton(Buttons[i]);
                break;
            }
        }
    }

    public void ChoseButton(Button button)
    {
        //ButtonToTrigger[CurrentButton]?.OnPointerExit(); // PointerEnter просто "ввод" он тригерит менеджер что бы тот сменил кнопку.
        //CurrentButtonIndex = buttonToIndex[button]; // а здесь (всмысле в функции) уже будет вызов конкретно логики выбора кнопки.
        //ButtonToTrigger[CurrentButton]?.OnPointerEnter();
    }

    public void InvokeButton()
    {
        CurrentButton.onClick.Invoke();
    }
}
