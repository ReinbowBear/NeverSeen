using UnityEngine;

public class ProxyState : MonoBehaviour, IInstaller
{
    public MonoBehaviour[] proxys;

    public void Install(Container container)
    {
        foreach (var proxy in proxys)
        {
            container.Add(proxy);
        }
    }
}
