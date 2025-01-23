using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChosenCharacter : MonoBehaviour
{
    [SerializeField] private EntityDataBase characters;
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
        EntitySO newCharacter = characters.containers[index];

        icon.sprite = newCharacter.UI.sprite;
        nameBar.text = newCharacter.UI.itemName;
        DescriptionBar.text = newCharacter.UI.description;

        health.text = newCharacter.health.ToString();
        armor.text = newCharacter.magicArmor.ToString();
        manna.text = newCharacter.manna.ToString();
        mannaMultiplier.text = newCharacter.mannaRegen.ToString();
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
