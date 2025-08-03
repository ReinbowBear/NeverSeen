using UnityEngine;
using UnityEngine.AddressableAssets;

public class BuildButton : MonoBehaviour
{
    [SerializeField] private AssetReference building;
    private GameObject LoadedBuilding;

    async void Start()
    {
        var handle = Loader.LoadAssetAsync<GameObject>(building.RuntimeKey.ToString());
        await handle;

        LoadedBuilding = handle.Result;
    }

    public void GetBuild()
    {
        Building building = Instantiate(LoadedBuilding).GetComponent<Building>();

        ClickController.Instance.NewBuilding = building;
        building.StartCoroutine(building.OnSpawn());
    }
}
