using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public static PlayerResource Instance;

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

    #region Other
    private void Load(OnLoad _)
    {

    }

    private void Save(OnSave _)
    {

    }


    void OnEnable()
    {
        EventBus.Add<OnLoad>(Load);
        EventBus.Add<OnSave>(Save);
    }

    void OnDisable()
    {
        EventBus.Remove<OnLoad>(Load);
        EventBus.Remove<OnSave>(Save);
    }
    #endregion
}

public enum ResourceType
{
    ore, technology
}