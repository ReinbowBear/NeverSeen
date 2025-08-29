using UnityEngine;
using UnityEngine.AddressableAssets;

public class BuildButton : MonoBehaviour
{
    [SerializeField] private AssetReference building;
    private GameObject LoadedBuilding;

    async void Start()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(building.RuntimeKey.ToString());
        await handle.Task;

        LoadedBuilding = handle.Result;
    }

    public void GetBuild()
    {
        Spawned building = Instantiate(LoadedBuilding).GetComponent<Spawned>();

        ClickHandler.Instance.NewBuilding = building;
        building.StartCoroutine(building.OnSpawn());
    }


    void OnDestroy()
    {
        Addressables.Release(building.RuntimeKey.ToString());
    }
}
