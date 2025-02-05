using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BattleMap : MonoBehaviour
{
    public Dictionary<bool, Transform[]> points = new Dictionary<bool, Transform[]>(); // isPlayer = true
    [SerializeField] private Transform[] CharacterPoints;
    [SerializeField] private Transform[] enemyPoints;

    public static BattleMap instance;
    private GameObject map;

    void Awake()
    {
        instance = this;
        points[true] = CharacterPoints;
        points[false] = enemyPoints;
    }


    public async void LoadMap(AssetReference asset)
    {
        map = await Address.GetAsset(asset, transform);
    }


    private void StartedMap(MyEvent.OnEntryBattle _)
    {
        byte index = SaveSystem.gameData.generalData.characteIndex;
        LoadMap(Content.instance.maps.containers[index]);
    }

    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntryBattle>(StartedMap);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEntryBattle>(StartedMap);
    }  
}
