using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject prefab;
    private Transform root;
    private Queue<GameObject> freeObjects;


    public ObjectPool(GameObject prefab, Transform newRoot = null)
    {
        this.prefab = prefab;
        freeObjects = new Queue<GameObject>();

        if (newRoot != null)
        {
            root = newRoot;
        }
        else
        {
            var poolObj = new GameObject(prefab.name + "_PoolRoot");
            root = poolObj.transform;
        }
    }


    public GameObject Get()
    {
        GameObject obj;

        if (freeObjects.Count > 0)
        {
            obj = freeObjects.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            obj = GameObject.Instantiate(prefab, root);
        }

        return obj;
    }


    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(root);
        freeObjects.Enqueue(obj);
    }
}
