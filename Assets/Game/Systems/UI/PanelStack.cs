using System.Collections.Generic;

public class PanelStack : ISystemData
{
    public Panel root;
    private Stack<Panel> PanelsStack = new();
    public Panel CurrentPanel => PanelsStack.Count > 0 ? PanelsStack.Peek() : null;

    // int newButtonIndex = (CurrentPanel.CurrentButtonIndex + direction + CurrentPanel.Buttons.Length) % CurrentPanel.Buttons.Length;
    public void OpenPanel(Panel panel)
    {
        PanelsStack.Push(panel);
        panel.Activate();
    }

    public bool ClosePanel()
    {
        if (CurrentPanel == null) return false;

        var panel = PanelsStack.Pop();
        panel.Deactivate();

        return true;
    }
}
