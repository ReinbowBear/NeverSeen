using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChosenCharacter : MonoBehaviour
{
    [SerializeField] private ContentSO contentSO;
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


    public void RenderCharacter(byte characterIndex)
    {
        chosenIndex = characterIndex;
        Container character = contentSO.data[chosenIndex];

        icon.sprite = character.UI.sprite;
        nameBar.text = character.UI.itemName;
        DescriptionBar.text = character.UI.description;

        CharacterSO characterStats = character.stats as CharacterSO; //приведение к конкретному типу, в случаи ошибки возращает null

        health.text = characterStats.health.ToString();
        armor.text = characterStats.armor.ToString();
        manna.text = characterStats.manna.ToString();
        mannaMultiplier.text = characterStats.mannaMultiplier.ToString();
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
