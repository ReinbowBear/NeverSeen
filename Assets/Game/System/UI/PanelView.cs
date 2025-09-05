using UnityEngine;

public class PanelView : MonoBehaviour
{
    [SerializeField] private Panel panel;
    [SerializeField] private AudioSO PanelSounds;

    [EventHandler(Priority.low)]
    private void PlaySound(OnPanelOpen panelEvent)
    {
        string soundName = panelEvent.isOpen ? "Open" : "Close";
        EventBus.Invoke(new OnSound(PanelSounds.GetByName(soundName)));

        if (panelEvent.isOpen) TweenAnimation.Spawn(panel.transform);
        else TweenAnimation.Destroy(panel.transform);
    }
}
