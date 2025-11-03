using System;
using UnityEngine.UI;

public class Focus
{
    public Button CurrentSelectedButton { get; private set; }
    public event Action<Button> OnButtonChanged;

    public void SelectButton(Button button)
    {
        if (CurrentSelectedButton == button) return;
        CurrentSelectedButton = button;
        OnButtonChanged?.Invoke(button);
    }
}
