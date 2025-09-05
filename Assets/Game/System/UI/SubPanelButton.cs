using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class SubPanelButton : Button
{
    [SerializeField] private Panel panel;
    public bool IsPresed { get; private set; }


    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        panel.SetActive(true);
        IsPresed = true;
    }

    public void ResetState()
    {
        IsPresed = false;
    }
}
