using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private EntityDataBase characters;
    [SerializeField] private byte characterIndex;
    [Space]
    [SerializeField] private ChosenCharacter viewCharacter;
    [SerializeField] private Image image;

    void Awake()
    {
        CharacterSO character = characters.containers[characterIndex];
        image.sprite = character.UI.sprite;
    }

    public void OnPointerEnter(PointerEventData pointerEventData) 
    {
        viewCharacter.RenderCharacter(characterIndex);
    }

    public void NewGame()
    {
        SaveSystem.DeleteSave();
        EventBus.Invoke<MyEvent.OnSave>();

        Scene.Load(1);
    }
}
