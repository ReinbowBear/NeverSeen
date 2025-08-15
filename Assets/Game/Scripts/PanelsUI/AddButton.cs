using UnityEngine;

public class AddButton : MonoBehaviour
{
    [SerializeField] private Panel panel;
    [Space]
    [SerializeField] private MyButton button;


    private void Load(OnLoad _)
    {
        panel.buttons.Add(button);
        button.gameObject.SetActive(true);

        MenuManager.instance.ChoseNewButton(panel.buttons.Count-1);
    }


    void OnEnable()
    {
        EventBus.Subscribe<OnLoad>(Load);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnLoad>(Load);
    }
}
