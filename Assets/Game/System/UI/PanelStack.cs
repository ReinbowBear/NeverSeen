using System.Collections.Generic;

public class PanelStack
{
    private Stack<Panel> PanelsStack = new();
    private Dictionary<Panel, IActivatable> panelToActivatable = new();

    public Panel CurrentPanel => PanelsStack.Count > 0 ? PanelsStack.Peek() : null;


    public void OpenPanel(Panel panel)
    {
        if(!panelToActivatable.TryGetValue(panel, out var activatable))
        {
            activatable = panel.gameObject.GetComponent<IActivatable>();
            panelToActivatable.Add(panel, activatable);
        }

        PanelsStack.Push(panel);
        activatable.Activate();
    }

    public void ClosePanel()
    {
        if (CurrentPanel == null) return;
        panelToActivatable[CurrentPanel].Deactivate();
        PanelsStack.Pop();
    }
}
