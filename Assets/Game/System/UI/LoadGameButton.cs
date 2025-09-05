using UnityEngine;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake()
    {
        button.gameObject.SetActive(false);
    }


    [EventHandler(Priority.low)]
    private void Load(OnLoad _)
    {
        button.gameObject.SetActive(true);
    }
}
