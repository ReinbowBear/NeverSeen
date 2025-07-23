using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowInfoOnUI : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private string characterName;
    [SerializeField] private Image image;

    void Awake()
    {
        //image.sprite = character.UI.sprite;
    }

    public void OnPointerEnter(PointerEventData pointerEventData) 
    {
        CharacterInfoUI.instance.RenderCharacter(characterName);
    }
}
