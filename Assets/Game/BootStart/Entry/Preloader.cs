using UnityEngine;
using Zenject;

public class Preloader : MonoBehaviour
{
    [SerializeField] private string[] toLoad;
    private Factory objectFactory;

    void Awake()
    {
        LoadAssets();
    }

    [Inject]
    public void Construct(Factory objectFactory)
    {
        this.objectFactory = objectFactory;
    }


    private async void LoadAssets()
    {
        foreach (var item in toLoad)
        {
            await objectFactory.Register(item);
        }

        Debug.Log("стартую OnSceneStart");
        EventBus.Invoke<OnSceneStart>();
    }
}
