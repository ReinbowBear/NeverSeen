using UnityEngine;
using UnityEngine.AddressableAssets;

public class ReleaseOnDestroy : MonoBehaviour
{
    public UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle handle;

    void OnDestroy()
    {
        if (handle.IsValid())
        {
            Addressables.ReleaseInstance(handle);
        }
    }
}
