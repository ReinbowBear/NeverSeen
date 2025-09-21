using UnityEngine;

public class Storage : MonoBehaviour, IBehavior
{
    private short size;

    public Storage(short newSize)
    {
        size = newSize;
    }


    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            ResourceManager.Instance.AddLimit(size);
        }
        else
        {
            ResourceManager.Instance.RemoveLimit(size);
        }
    }

    public void AddResources(ResourceType type, short value)
    {
        ResourceManager.Instance.RefreshResource(type, value);
    }
}
