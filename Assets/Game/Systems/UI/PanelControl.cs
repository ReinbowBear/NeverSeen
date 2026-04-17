using System.Collections.Generic;

public class PanelControl : ISystem
{
    private UIInput input;

    private Stack<Panel> PanelsStack = new();
    public Panel CurrentPanel => PanelsStack.Count > 0 ? PanelsStack.Peek() : null;

    public PanelControl(UIInput input)
    {
        this.input = input;
    }

    public void SetSubs(SystemSubs subs)
    {
        subs.AddWithCommands<Panel>(OnEsc).OnEvent<OnNavigate>();
    }


    public void OnEsc(EntityCommands commands, Panel panel)
    {
        if (!input.Esc) return;

        ClosePanel();
        commands.AddOneFrame<OnPanelClose>();
    }



    public void OpenPanel(Panel panel)
    {
        PanelsStack.Push(panel);
        
        panel.canvasGroup.interactable = true;
        panel.canvasGroup.blocksRaycasts = true;
    }

    public void ClosePanel()
    {
        if (CurrentPanel == null) return;

        var panel = PanelsStack.Pop();

        panel.canvasGroup.interactable = false;
        panel.canvasGroup.blocksRaycasts = false;
    }
}
