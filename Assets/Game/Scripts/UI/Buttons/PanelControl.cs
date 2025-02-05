using UnityEngine;

public class PanelControl : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private MenuKeyboard menuKeyboard;

    public void SetPanel()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            menuKeyboard.panels.Remove(this);
        }
        else
        {
            panel.SetActive(true);
            menuKeyboard.panels.Add(this);
        }
    }
}
