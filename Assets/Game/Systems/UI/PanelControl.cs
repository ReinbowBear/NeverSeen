using System.Collections.Generic;

public class PanelControl
{
    private Stack<Panel> PanelsStack = new();
    public Panel CurrentPanel => PanelsStack.Count > 0 ? PanelsStack.Peek() : null;

    // int newButtonIndex = (CurrentPanel.CurrentButtonIndex + direction + CurrentPanel.Buttons.Length) % CurrentPanel.Buttons.Length;
    public void OpenPanel(Panel panel)
    {
        PanelsStack.Push(panel);
        panel.Activate();
    }

    public bool ClosePanel(out Panel panel)
    {
        if (CurrentPanel == null)
        {
            panel = null;
            return false;
        }

        panel = PanelsStack.Pop();
        panel.Deactivate();
        return true;
    }
}
