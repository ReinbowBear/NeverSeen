using UnityEngine;

public class UIInstaller : MonoBehaviour, IInstaller
{
    public void Install(Container container)
    {
        container.Add(gameObject.AddComponent<ProxyPanelControl>());
        container.Add(gameObject.AddComponent<ProxyNavigate>());
    }
}