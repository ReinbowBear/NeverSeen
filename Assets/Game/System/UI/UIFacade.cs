using UnityEngine;

public class UIFacade : MonoBehaviour
{
    private PanelStack panelStack;

    public void OpenPanel(Panel panelControl) // а где эффекты?
    {
        //PanelsStack.Push(panelControl);
        //OnPanelOpen.Invoke();
    }

    private void ClosePanel() // реализация открытия закрытия может быть разной, это другой компонент
    {
        //if (CurrentPanel == null) return;
        //PanelsStack.Pop();
        //OnPanelOClose.Invoke();
    }


    public void ActiveInput()
    {
        //if (isActiveInputs) return;
        //input.UI.Navigate.started += NavigateInput;
        //input.UI.Submit.started += InvokeButton;
        //input.System.Esc.started += ClosePanel;
        //isActiveInputs = true;
    }

    public void DisableInput()
    {
        //if (!isActiveInputs) return;
        //input.UI.Navigate.started -= NavigateInput;
        //input.UI.Submit.started -= InvokeButton;
        //input.System.Esc.started -= ClosePanel;
        //isActiveInputs = false;
    }
}

public class PanelData
{
    public Panel panel;
    public IActivatable activatable;
}
