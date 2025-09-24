using System.Collections;
using UnityEngine;
using Zenject;

public class Preloader : MonoBehaviour
{
    [SerializeField] private string[] preloadKeys;
    private bool isInit;

    [Inject] private Factory factory;

    async void Awake()
    {
        foreach (var key in preloadKeys)
        {
            await factory.GetAsset(key);
        }
        isInit = true;
    }

    void Start()
    {
        StartCoroutine(WaitToStart()); 
    }


    private IEnumerator WaitToStart()
    {
        yield return new WaitUntil(() => isInit == true);
        EventBus.Invoke<OnSceneStart>();
    }


    void OnDestroy()
    {
        factory.Clear();
    }
}
