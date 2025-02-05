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
    [SerializeField] private TextMeshProUGUI mannaMultiplier;

    private byte chosenIndex;


    public void RenderCharacter(byte index)
    {
        chosenIndex = index;
        CharacterSO newCharacter = characters.containers[index];

        icon.sprite = newCharacter.UI.sprite;
        nameBar.text = newCharacter.UI.itemName;
        DescriptionBar.text = newCharacter.UI.description;

        health.text = newCharacter.health.ToString();
        armor.text = newCharacter.armor.ToString();
        mannaMultiplier.text = newCharacter.reloadMultiplier.ToString();
    }


    private void Save(MyEvent.OnSave _)
    {
        SaveSystem.gameData.generalData.characteIndex = chosenIndex;
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnSave>(Save);
    }

    void OnDisable()
    {
         EventBus.Remove<MyEvent.OnSave>(Save);
    }
}

[System.Serializable]
public struct SaveChosenCharacter
{
    public byte chosenIndex;
}
