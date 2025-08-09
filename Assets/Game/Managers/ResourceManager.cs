using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    [SerializeField] private int MaxResource;
    private Dictionary<ResourceType, int> resources = new();
    private List<Miner> miners = new();
    [SerializeField] private float TickRate;
    [Space]
    [SerializeField] private SerializableDictionary<ResourceType, MyBar> bars;

    private Coroutine coroutine;

    void Awake()
    {
        Instance = this;
    }


    public void RefreshResource(ResourceType type, short value)
    {
        resources[type] += value;
        bars[type].SetValue(resources[type], MaxResource);
    }

    #region miners
    public void AddBuilding(Miner newMiner)
    {
        if (miners.Contains(newMiner)) return;

        miners.Add(newMiner);

        if (coroutine == null)
        {
            coroutine = StartCoroutine(CollectResources());
        }
    }

    public void RemoveBuilding(Miner miner)
    {
        if (!miners.Contains(miner)) return;

        miners.Remove(miner);

        if (miners.Count == 0)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private IEnumerator CollectResources()
    {
        while (true)
        {
            foreach (var miner in miners)
            {
                miner.Work();
            }

            yield return new WaitForSeconds(TickRate);
        }
    }
    #endregion

    #region storage
    public void AddLimit(short value)
    {
        MaxResource += value;
    }

    public void RemoveLimit(short value)
    {
        MaxResource -= value;
    }
    #endregion
}

public enum ResourceType
{
    ore, technology
}