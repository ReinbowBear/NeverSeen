using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : MonoBehaviour, IPointerEnterHandler
{
    public Button button;
    [SerializeField] private TextMeshProUGUI text;
    [Space]
    [SerializeField] private byte index;
    [Space]
    [SerializeField] private AudioClip choseSound;
    [SerializeField] private AudioClip pressSound;

    void Start()
    {
        button.onClick.AddListener(InvokeSound);
    }


    public void Trigger(float fade, bool isChose)
    {
        if (isChose == true)
        {
            Debug.Log("у нас тут скрипт звуков реворкнут был (почти) надо разобратся");
            //Sound.instance.Play(choseSound);
        }

        text.DOFade(fade, 0.5f)
        .SetLink(text.gameObject)
        .SetUpdate(true); // игнорирует тайм скейл, то бишь паузу
    }


    private void InvokeSound()
    {
        if (pressSound != null)
        {
            Debug.Log("у нас тут скрипт звуков реворкнут был (почти) надо разобратся");
            //Sound.instance.Play(pressSound);
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData) 
    {
        MenuManager.instance.ChoseNewButton(index);
    }


    void OnDisable()
    {
        button.onClick.RemoveListener(InvokeSound);
    }
}
