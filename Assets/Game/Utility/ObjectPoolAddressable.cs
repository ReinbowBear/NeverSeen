using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectPoolAddressable
{
    private string addressKey;
    private Transform root;

    private Queue<GameObject> freeObjects;
    private Factory factory = new();

    public ObjectPoolAddressable(string newAddressKey, Transform newRoot = null)
    {
        addressKey = newAddressKey;
        freeObjects = new();

        root = newRoot;
        if (newRoot != null) return;

        var poolObj = UnityEngine.Object.Instantiate(new GameObject());
        root = poolObj.transform;
        poolObj.name = newAddressKey + "_Root";
    }


    public async Task<GameObject> Get()
    {
        GameObject obj;
        if (freeObjects.Count > 0)
        {
            obj = freeObjects.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            await factory.LoadAsync(addressKey);
            obj = (GameObject)factory.Instantiate(addressKey);

            obj.transform.SetParent(root);
        }

        return obj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        freeObjects.Enqueue(obj);
    }
}
