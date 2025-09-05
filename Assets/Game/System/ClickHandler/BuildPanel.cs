using UnityEngine;
using Zenject;

public class BuildPanel : MonoBehaviour
{
    [SerializeField] private ClickHandler clickHandler;
    [SerializeField] private string[] building;

    private Factory factory;
    private InventoryData inventoryData;
    private bool isInit;

    [Inject]
    public void Construct(Factory factory, GameData gameData)
    {
        this.factory = factory;
        this.inventoryData = gameData.inventory;
    }

    async void Start()
    {
        foreach (var item in building) // подставить инвентарь потом
        {
            await factory.Register(item);
        }
        isInit = true;
    }


    public void GetBuilding(string buildingName)
    {
        if (!isInit) return;

        clickHandler.SetMode(ViewMode.edit);
        var mode = clickHandler.editMode as EditMode;

        var obj = factory.Create(buildingName);
        mode.SetBuilding(obj.GetComponent<Building>());
    }
}
