using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private readonly Dictionary<string, Queue<GameObject>> objectPools = new();

    private void Awake()
    {
        Instance = this;
    }


    public async Task<GameObject> GetObjectAsync(string key, Vector3 position = default, Quaternion rotation = default)
    {
        if (!objectPools.TryGetValue(key, out var pool))
        {
            pool = new Queue<GameObject>();
            objectPools[key] = pool;
        }

        if (pool.Count > 0)
        {
            var obj = pool.Dequeue();
            obj.SetActive(true);
            obj.transform.SetPositionAndRotation(position, rotation);
            return obj;
        }

        var prefab = await Loader.LoadAssetAsync<GameObject>(key);
        var instance = Instantiate(prefab, position, rotation);
        return instance;
    }


    public void ReturnObject(string key, GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        if (!objectPools.ContainsKey(key))
        {
            objectPools[key] = new Queue<GameObject>();
        }

        objectPools[key].Enqueue(obj);
    }


    public void ClearAll() // скорее понадобится только очистка списка для переходов между сценами например
    {
        foreach (var queue in objectPools.Values)
        {
            foreach (var obj in queue)
            {
                Destroy(obj);
            }
        }

        objectPools.Clear();
    }
}
