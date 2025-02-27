using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public GameObject navigateObject;
    [Space]
    public List<MyButton> buttons;
    [HideInInspector] public int chosenButton;

    public void OpenPanel()
    {
        gameObject.SetActive(true);

        MenuKeyboard.instance.panels.Add(this);
        MenuKeyboard.instance.CheckPanel();
        MenuKeyboard.instance.ChoseNewButton(0);
    }

    public void ClosePanel()
    {
        MenuKeyboard.instance.panels.Remove(this);
        MenuKeyboard.instance.CheckPanel();

        gameObject.SetActive(false);
    }
}
