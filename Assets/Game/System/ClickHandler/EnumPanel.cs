using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class EnumPanel : MonoBehaviour
{
    public GameObject ItemPref;

    [Inject] private Factory factory;
    [Inject] private World world;
    [Inject] private InventoryData inventory;

    private Dictionary<string, MyButton> Buttons = new();
    private Dictionary<MyButton, UnityAction> SubLamdas = new();

    public void CheckItems()
    {
        foreach (var key in inventory.buildings.Keys)
        {
            if (Buttons.ContainsKey(key)) continue;
            AddBuilding(key);
        }
    }

    public async void AddBuilding(string buildingName)
    {
        await factory.GetAsset(buildingName);

        var buttonObj = Instantiate(ItemPref, transform);
        var Button = buttonObj.GetComponent<MyButton>();

        UnityAction action = () => GetBuilding(buildingName);
        SubLamdas.Add(Button, action);
        Button.onClick.AddListener(action);
    }


    public async void GetBuilding(string buildingName)
    {
        if (world.ChosenEntity != null) return;

        var obj = await factory.Create(buildingName);
        world.ChosenEntity = obj.GetComponent<Entity>();

        var mouseFollow = obj.AddComponent<MouseFollowView>();
        mouseFollow.Init(obj, LayerMask.GetMask("Tile"), null);

        var showTiles = obj.AddComponent<ShowTilesView>();
        showTiles.Radius = world.ChosenEntity.Stats.Radius;
        mouseFollow.OnMoveToTarget.AddListener(showTiles.ShowTiles);
        mouseFollow.OnStopFollowing.AddListener(showTiles.HideTiles);

        factory.Inject(showTiles);
        mouseFollow.StartFollow();

        EventBus.Invoke<OnNewEntity>();
    }


    void OnDisable()
    {
        foreach (var keyButton in SubLamdas.Keys)
        {
            var action = SubLamdas[keyButton];
            keyButton.onClick.RemoveListener(action);
        }
    }
}
