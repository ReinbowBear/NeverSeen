using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : Button
{
    public Action OnButtonEnter;
    public Action OnButtonExit;

    public override void OnPointerEnter(PointerEventData pointerEventData)
    {
        base.OnPointerEnter(pointerEventData);
        OnEnter();
    }


    public void OnEnter()
    {
        OnButtonEnter?.Invoke();
    }

    public void OnExit()
    {
        OnButtonExit?.Invoke();
    }
}
