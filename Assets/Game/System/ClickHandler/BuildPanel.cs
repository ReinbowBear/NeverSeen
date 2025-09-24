using UnityEngine;
using Zenject;

public class BuildPanel : MonoBehaviour
{
    [SerializeField] private ClickHandler clickHandler;

    [Inject] private Factory factory;
    [Inject] private TileMapData mapData;
    [Inject] private InventoryData inventory;


    public async void GetBuilding(string buildingName)
    {
        if (mapData.CurrentEntity != null) return;

        var obj = await factory.Create(buildingName);
        mapData.CurrentEntity = obj.GetComponent<Entity>();

        var mouseFollow = obj.AddComponent<MouseFollowView>();
        mouseFollow.Init(obj, LayerMask.GetMask("Tile"), null);

        var showTiles = obj.AddComponent<ShowTilesView>();
        showTiles.Radius = mapData.CurrentEntity.Stats.Radius;
        mouseFollow.OnMoveToTarget.AddListener(showTiles.ShowTiles);
        mouseFollow.OnStopFollowing.AddListener(showTiles.HideTiles);

        factory.Inject(showTiles);
        mouseFollow.StartFollow();

        clickHandler.SetMode<EditMode>();
        EventBus.Invoke<OnNewEntity>();
    }
}
