using UnityEngine;
using Zenject;

public class PanelView : MonoBehaviour
{
    [SerializeField] private Panel panel;
    [SerializeField] private AudioSO OpenSounds;
    [SerializeField] private AudioSO CloseSounds;

    private AudioService audioService;

    [Inject]
    public void Construct(AudioService audioService)
    {
        this.audioService = audioService;
    }

    private void OnPanelOpenClose(bool IsOpen)
    {
        var soundSO = IsOpen ? OpenSounds : CloseSounds;
        audioService.Play(soundSO);;

        if (IsOpen) Tween.Spawn(panel.transform);
        else Tween.Destroy(panel.transform);
    }


    void Awake() => panel.OnPanelToggle += OnPanelOpenClose;
    void OnDestroy() => panel.OnPanelToggle -= OnPanelOpenClose;
}
