using UnityEngine;

public class PanelView : MonoBehaviour
{
    [SerializeField] private Panel panel;
    [SerializeField] private AudioSO PanelSounds;

    private void OnPanelOpenClose(bool IsOpen)
    {
        string soundName = IsOpen ? "Open" : "Close";
        EventBus.Invoke(PanelSounds.GetByName(soundName));

        if (IsOpen) Tween.Spawn(panel.transform);
        else Tween.Destroy(panel.transform);
    }


    void Awake() => panel.OnPanelToggle += OnPanelOpenClose;
    void OnDestroy() => panel.OnPanelToggle -= OnPanelOpenClose;
}
