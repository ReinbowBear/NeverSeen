using UnityEngine;

public class CoreInstaller : MonoBehaviour, IInstaller
{
    public void Install(Container container)
    {
        container.Add(gameObject.AddComponent<ProxyInput>());
        container.Add(gameObject.AddComponent<ProxySceneLoader>());
    }
}
