using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanelStack : MonoBehaviour
{
    public UnityEvent OnPanelOpen;
    public UnityEvent OnPanelOClose;

    private Stack<Panel> PanelsStack = new();
    public Panel CurrentPanel => PanelsStack.Count > 0 ? PanelsStack.Peek() : null;


    public void OpenPanel(Panel panelControl) // а где эффекты?
    {
        PanelsStack.Push(panelControl);
        OnPanelOpen.Invoke();
    }

    private void ClosePanel() // реализация открытия закрытия может быть разной, это другой компонент
    {
        if (CurrentPanel == null) return;
        PanelsStack.Pop();
        OnPanelOClose.Invoke();
    }
}
