using System.Collections.Generic;
using UnityEngine;

public class PanelControl : ISystem
{
    private Stack<Panel> PanelsStack = new();
    public Panel CurrentPanel => PanelsStack.Count > 0 ? PanelsStack.Peek() : null;

    public void Execute(World world, EntityCommands commands)
    {
        if (!world.Has<IntentEsc>()) return;

        foreach (var (esc, entity) in world.Query<CurrentPanel>().Exclude<RootPanel>())
        {
            if (!ClosePanel()) return;

            commands.RemoveComponent<CurrentPanel>(entity);
            commands.AddOneFrame(new OnPanelClose());
        }
    }


    private void OpenPanel(Panel panel)
    {
        PanelsStack.Push(panel);
        
        panel.canvasGroup.interactable = true;
        panel.canvasGroup.blocksRaycasts = true;
    }

    private bool ClosePanel()
    {
        if (CurrentPanel == null) return false;

        var panel = PanelsStack.Pop();

        panel.canvasGroup.interactable = false;
        panel.canvasGroup.blocksRaycasts = false;

        return true;
    }
}
