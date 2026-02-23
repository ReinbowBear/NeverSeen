using UnityEngine;

public class ViewInstaller : MonoBehaviour, IInstaller
{
    public void Install(Container container)
    {
        container.Add(gameObject.AddComponent<ProxyAudio>());
        container.Add(gameObject.AddComponent<ProxyTween>());
    }
}
