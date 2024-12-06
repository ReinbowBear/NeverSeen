using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] private ViewCharacter viewCharacter;
    [SerializeField] private Transform characterBar;
    [SerializeField] private GameObject prefab;
    
    void Start()
    {
        MakeCharacterButton();
    }

    private void MakeCharacterButton()
    {
        for (byte i = 0; i < Content.data.characters.Length; i++)
        {
            CharacterButton newCharacterButton = Instantiate(prefab, characterBar).GetComponent<CharacterButton>();
            newCharacterButton.viewCharacter = viewCharacter;
            
            newCharacterButton.image.sprite = Content.data.characters[i].sprite;
            newCharacterButton.character = Content.data.characters[i];
        }
    }
}
