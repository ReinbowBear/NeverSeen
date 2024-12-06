using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour, IPointerEnterHandler
{
    [HideInInspector] public ViewCharacter viewCharacter;

    public Image image;
    [HideInInspector] public ItemSO character;

    public void OnPointerEnter(PointerEventData pointerEventData) 
    {
        viewCharacter.RenderCharacter(character);
    } 

    public void NewGame()
    {
        SaveSystem.DeleteSave();
        Scene.Load(1);
    }
}
