using UnityEngine;
using Zenject;

public class GetBuilding : MonoBehaviour
{
    [Inject] EventBus eventBus;
    [Inject] private Factory factory;
    [Inject] private WorldOld world;

    public async void GetBuildingToMouse(string buildingName)
    {
        if (world.ChosenEntity != null) return;

        var obj = await factory.Create(buildingName);
        world.ChosenEntity = obj.GetComponent<EntityOld>();

        var mouseFollow = obj.AddComponent<MouseFollowView>();
        mouseFollow.Init(obj, LayerMask.GetMask("Tile"), null);

        if (world.ChosenEntity.TryGetComponent<IHaveRadius>(out var component))
        {
            var showTiles = obj.AddComponent<ShowTilesView>();
            showTiles.Radius = component.GetRadius();

            mouseFollow.OnMoveToTarget.AddListener(showTiles.ShowTiles);
            mouseFollow.OnStopFollowing.AddListener(showTiles.HideTiles);

            factory.Inject(showTiles);
        }

        mouseFollow.StartFollow();
        eventBus.Invoke<OnNewEntity>();
    }
}
