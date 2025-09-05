using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioSO ButtonSounds;
    [Space]
    [SerializeField] private RectTransform navigateObject;
    private float navigateTime = 0.1f;
    private float animationOffset = 50;
    private bool isButtonClicked;


    private void OnButtonClick()
    {
        EventBus.Invoke(new OnSound(ButtonSounds.GetByName("Click")));
        Tween.MoveFromCenter(button.transform, new Vector3(animationOffset, 0, 0));
        isButtonClicked = !isButtonClicked;
    }

    public void OnButtonChose(bool isChose)
    {
        if (isChose)
        {
            EventBus.Invoke(new OnSound(ButtonSounds.GetByName("Chose")));
            CoroutineManager.Start(MoveToButton(button.transform), this);
        }

        float fade = isChose ? 0.9f : 0.5f;

        text.DOFade(fade, 0.5f)
        .SetLink(text.gameObject)
        .SetUpdate(true); // игнорирует тайм скейл, то бишь паузу
    }


    private IEnumerator MoveToButton(Transform target)
    {
        Vector3 startPos = navigateObject.position;
        Vector3 endPos = new Vector3(startPos.x, target.position.y, startPos.z); // анимация навигации идёт только по Y!

        float timeElapsed = 0f;

        while (timeElapsed < navigateTime)
        {
            navigateObject.position = Vector3.Lerp(startPos, endPos, timeElapsed / navigateTime);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        navigateObject.position = endPos;
    }


    public void OnPointerEnter(PointerEventData _) => OnButtonChose(true); // интерфейсы наведения мышкой
    public void OnPointerExit(PointerEventData _) => OnButtonChose(false);

    public void OnSelect(BaseEventData _) => OnButtonChose(true); // интерфейсы "наведения" с клавиатуры
    public void OnDeselect(BaseEventData _) => OnButtonChose(false);


    void OnEnable() => button.onClick.AddListener(OnButtonClick);
    void OnDisable() => button.onClick.RemoveListener(OnButtonClick);
}
