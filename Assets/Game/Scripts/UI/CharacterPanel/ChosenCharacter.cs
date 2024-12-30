using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChosenCharacter : MonoBehaviour
{
    [SerializeField] private CharacterDataBase characters;
    [Space]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameBar;
    [SerializeField] private TextMeshProUGUI DescriptionBar;
    [Space]
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI armor;
    [SerializeField] private TextMeshProUGUI manna;
    [SerializeField] private TextMeshProUGUI mannaMultiplier;

    private byte chosenIndex;


    public void RenderCharacter(byte index)
    {
        chosenIndex = index;
        CharacterContainer newCharacter = characters.containers[index] as CharacterContainer; //приведение к конкретному типу, в случаи ошибки возращает null

        icon.sprite = newCharacter.UI.sprite;
        nameBar.text = newCharacter.UI.itemName;
        DescriptionBar.text = newCharacter.UI.description;

        health.text = newCharacter.stats.health.ToString();
        armor.text = newCharacter.stats.armor.ToString();
        manna.text = newCharacter.stats.manna.ToString();
        mannaMultiplier.text = newCharacter.stats.mannaScale.ToString();
    }


    private void Save()
    {
        SaveChosenCharacter saveChosenCharacter = new SaveChosenCharacter();
        saveChosenCharacter.chosenIndex = chosenIndex;

        SaveSystem.gameData.saveChosenCharacter = saveChosenCharacter;
    }


    void OnEnable()
    {
        SaveSystem.onSave += Save;
    }

    void OnDisable()
    {
        SaveSystem.onSave -= Save;
    }
}

[System.Serializable]
public struct SaveChosenCharacter
{
    public byte chosenIndex;
}
