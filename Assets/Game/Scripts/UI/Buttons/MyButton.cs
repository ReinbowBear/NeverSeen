using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;
    [Space]
    [SerializeField] private byte index;

    public void OnPointerEnter(PointerEventData pointerEventData) 
    {
        MenuKeyboard.instance.ChoseNewButton(index);
    }


    public void Invoke()
    {
        button.onClick.Invoke();
    }

    public void Triggered(float fade)
    {
        text.DOFade(fade, 0.5f)
        .SetLink(text.gameObject)
        .SetUpdate(true); // игнорирует тайм скейл, то бишь паузу
    }
}
