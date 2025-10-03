using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextFadeView : MonoBehaviour
{
    [Header("Ref")]
    public MyButton button;
    public TextMeshProUGUI text;
    public SoundSO ClickSounds;
    public SoundSO ChoseSounds;

    [Header("Settings")]
    public float EnterFadeValue = 0.9f;
    public float ExitFadeValue = 0.5f;
    public float animationTime = 0.2f;


    private void OnButtonEnter() => DoTextFade(EnterFadeValue);
    private void OnButtonExit() => DoTextFade(ExitFadeValue);

    private void DoTextFade(float fadeValue)
    {
        text.DOFade(fadeValue, animationTime)
        .SetLink(text.gameObject)
        .SetUpdate(true); // игнорирует тайм скейл, то бишь паузу
    }


    void OnEnable()
    {
        button.OnButtonEnter += OnButtonEnter;
        button.OnButtonExit += OnButtonExit;
    }

    void OnDisable()
    {
        button.OnButtonEnter -= OnButtonEnter;
        button.OnButtonExit -= OnButtonExit;
    }
}
