using UnityEngine;
using Zenject;

public class Preloader : MonoBehaviour
{
    [Inject] private InventoryData inventory;
    [Inject] private Factory factory;

    async void Awake()
    {
        await factory.GetAsset("Generator");
        await factory.GetAsset("Miner");
        await factory.GetAsset("Storage");

        EventBus.Invoke<OnSceneStart>();
    }


    void OnDestroy()
    {
        factory.Clear();
    }
}
