using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : Button
{
    public Action<MyButton> OnButtonEnter;
    public Action OnButtonExit;

    public override void OnPointerEnter(PointerEventData pointerEventData) // интерфейсы наведения мышкой
    {
        base.OnPointerEnter(pointerEventData);
        OnEnter();
    }


    public void OnEnter()
    {
        OnButtonEnter?.Invoke(this);
    }

    public void OnExit() // панель следит за наведением на новую кнопку и отключает старые
    {
        OnButtonExit?.Invoke();
    }
}
