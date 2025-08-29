using UnityEngine;
using UnityEngine.AddressableAssets;

public class Preloader : MonoBehaviour
{
    [SerializeField] private AssetReference[] toLoad;

    void Awake()
    {
        LoadAssets();
    }


    private async void LoadAssets()
    {
        foreach (var item in toLoad)
        {
            await ObjectPool.Register(item.RuntimeKey.ToString());
        }

        EventBus.Invoke<OnSceneStart>();
    }
}
