using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewCharacter : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameBar;

    public void RenderCharacter(ItemSO character)
    {
        nameBar.text = character.name;
        icon.sprite = character.sprite;
    }
}
