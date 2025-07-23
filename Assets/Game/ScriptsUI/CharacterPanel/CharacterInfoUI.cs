using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    public static CharacterInfoUI instance;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameBar;
    [SerializeField] private TextMeshProUGUI DescriptionBar;
    [Space]
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI armor;
    [SerializeField] private TextMeshProUGUI mannaMultiplier;

    private string chosenCharacter;

    void Awake()
    {
        instance = this;   
    }


    public void RenderCharacter(string index)
    {
        //chosenCharacter = index;
        //List<string> characters = AddressImporter.addressStorage["Characters"];
        //string newCharacter = characters[index];
//
        //icon.sprite = newCharacter.UI.sprite;
        //nameBar.text = newCharacter.UI.itemName;
        //DescriptionBar.text = newCharacter.UI.description;
//
        //health.text = newCharacter.health.ToString();
    }


    private void Save(OnSave _)
    {
        SaveSystem.gameData.generalData.character = chosenCharacter;
    }


    void OnEnable()
    {
        EventBus.Add<OnSave>(Save);
    }

    void OnDisable()
    {
        EventBus.Remove<OnSave>(Save);
    }
}
