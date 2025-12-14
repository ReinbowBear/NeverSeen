using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public Button[] Buttons;
    public Button CurrentButton => Buttons[CurrentButtonIndex];
    public int CurrentButtonIndex { get; private set; }


    public void ChoseButtonByIndex(int index)
    {
        for (int i = index; i < Buttons.Length; i++)
        {
            if (Buttons[i].gameObject.activeInHierarchy)
            {
                CurrentButtonIndex = i;
                break;
            }
        }
    }

    public void InvokeButton()
    {
        CurrentButton.onClick.Invoke();
    }
}
