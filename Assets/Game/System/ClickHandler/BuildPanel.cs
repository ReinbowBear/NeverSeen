using UnityEngine;
using Zenject;

public class BuildPanel : MonoBehaviour
{
    [SerializeField] private ClickHandler clickHandler;
    [SerializeField] private Factory factory;

    private GameMapData gameMap;
    private InventoryData inventory;

    [Inject]
    public void Construct(GameData gameData)
    {
        gameMap = gameData.GameMap;
        inventory = gameData.Inventory;
    }


    public async void GetBuilding(string buildingName)
    {
        clickHandler.StateMachine.SetMode<DefaultMode>();

        var obj = await factory.Create(buildingName);
        gameMap.CurrentBuilding = obj.GetComponent<Building>();

        EventBus.Invoke<OnNewBuilding>();
    }
}
