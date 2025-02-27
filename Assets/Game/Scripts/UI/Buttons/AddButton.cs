using UnityEngine;

public class AddButton : MonoBehaviour
{
    [SerializeField] private Panel panel;
    [Space]
    [SerializeField] private MyButton button;


    private void Load(MyEvent.OnLoad _)
    {
        panel.buttons.Add(button);
        button.gameObject.SetActive(true);

        MenuKeyboard.instance.ChoseNewButton(panel.buttons.Count-1);
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnLoad>(Load);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnLoad>(Load);
    }
}
