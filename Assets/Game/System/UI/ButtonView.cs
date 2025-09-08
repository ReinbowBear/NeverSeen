using DG.Tweening;
using TMPro;
using UnityEngine;

public class ButtonView : MonoBehaviour
{
    [SerializeField] private MyButton button;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioSO ButtonSounds;
    [SerializeField] bool isOpenPanelButton;
    [Space]
    [SerializeField] private float animationOffset = 50;
    [SerializeField] private float animationTime = 0.2f;

    private Vector3 originalPos;

    void Awake()
    {
        originalPos = button.transform.position;
    }


    private void OnButtonClick()
    {
        if (isOpenPanelButton) return;
        EventBus.Invoke(ButtonSounds.GetByName("Click"));
    }

    private void OnButtonEnter(MyButton button)
    {
        EventBus.Invoke(ButtonSounds.GetByName("Chose"));
        Tween.MoveToPosition(button.transform, originalPos + new Vector3(animationOffset, 0, 0), animationTime);
        DoTextFade(0.9f);
    }

    private void OnButtonExit()
    {
        Tween.MoveToPosition(button.transform, originalPos, animationTime);
        DoTextFade(0.5f);
    }


    private void DoTextFade(float fadeValue)
    {
        text.DOFade(fadeValue, animationTime)
        .SetLink(text.gameObject)
        .SetUpdate(true); // игнорирует тайм скейл, то бишь паузу
    }


    void OnEnable()
    {
        button.onClick.AddListener(OnButtonClick);
        button.OnButtonEnter += OnButtonEnter;
        button.OnButtonExit += OnButtonExit;
    }

    void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClick);
        button.OnButtonEnter -= OnButtonEnter;
        button.OnButtonExit -= OnButtonExit;
    }
}
