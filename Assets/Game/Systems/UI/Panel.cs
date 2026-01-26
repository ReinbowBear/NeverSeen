using UnityEngine;

public class Panel : MonoBehaviour, IActivatable
{
    public CanvasGroup canvasGroup;

    public void Activate()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Deactivate()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
