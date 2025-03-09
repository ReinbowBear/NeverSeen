using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChosenCharacter : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameBar;
    [SerializeField] private TextMeshProUGUI DescriptionBar;
    [Space]
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI armor;
    [SerializeField] private TextMeshProUGUI mannaMultiplier;

    private string chosenIndex;


    public void RenderCharacter(byte index)
    {
        //chosenIndex = index;
        //Entity newCharacter = null; //characters.containers[index];
//
        //icon.sprite = newCharacter.UI.sprite;
        //nameBar.text = newCharacter.UI.itemName;
        //DescriptionBar.text = newCharacter.UI.description;
//
        //health.text = newCharacter.health.ToString();
    }


    private void Save(MyEvent.OnSave _)
    {
        SaveSystem.gameData.generalData.character = chosenIndex;
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
