using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameMap : MonoBehaviour
{
    private GameObject map;

    public async void LoadMap(AssetReference asset)
    {
        var handle = Addressables.InstantiateAsync(asset, transform);
        await handle.Task;

        var release = handle.Result.AddComponent<ReleaseOnDestroy>();
        release.handle = handle;

        map = handle.Result;
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
