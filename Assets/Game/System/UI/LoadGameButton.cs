using UnityEngine;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button button;

    private void Awake()
    {
        button.gameObject.SetActive(false);
    }


    private void Load(OnLoad _)
    {
        button.gameObject.SetActive(true);
    }
}
