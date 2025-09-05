using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    public Button[] Buttons => buttons;
    public Button CurrentButton => EventSystem.current.currentSelectedGameObject?.GetComponent<Button>();
    private GameObject previousSelected;


    public void SetActive(bool isTrue)
    {
        gameObject.SetActive(isTrue);
        SetNavigation(isTrue);

        if (isTrue)
        {
            RestoreOrSelectButton();
        }
        else
        {
            previousSelected = null;
        }

        EventBus.Invoke(new OnPanelOpen(this, isTrue));
    }

    public void SetNavigation(bool isEnable)
    {
        var newMode = isEnable ? Navigation.Mode.Automatic : Navigation.Mode.None;

        foreach (var button in buttons)
        {
            var nav = button.navigation;
            nav.mode = newMode;
            button.navigation = nav;
        }
    }


    private void RestoreOrSelectButton()
    {
        if (previousSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(previousSelected);
            return;
        }

        foreach (var button in buttons)
        {
            if (button.gameObject.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(button.gameObject);
                break;
            }
        }
    }
}
