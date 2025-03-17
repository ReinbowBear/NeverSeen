using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameMap : MonoBehaviour
{
    private GameObject map;

    public async void LoadMap(AssetReference asset)
    {
        map = await Address.GetAsset(asset);
        map.transform.SetParent(transform);
    }


    private void StartedMap(OnEntryBattle _)
    {
        //LoadMap(Content.instance.maps.containers[index]);
    }

    void OnEnable()
    {
        EventBus.Add<OnEntryBattle>(StartedMap);
    }

    void OnDisable()
    {
        EventBus.Remove<OnEntryBattle>(StartedMap);
    }  
}
