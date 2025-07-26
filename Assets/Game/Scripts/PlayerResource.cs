using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public static PlayerResource Instance;

    public Dictionary<ResourceType, int> resources;
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
        miners.Add(newMiner);
        if (coroutine == null)
        {
            coroutine = StartCoroutine(CollectResources());
        }
    }

    public void RemoveBuilding(Miner miner)
    {
        miners.Remove(miner);
        if (miners.Count == 0)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }


    private IEnumerator CollectResources()
    {
        while (miners.Count != 0)
        {
            foreach (var miner in miners)
            {
                miner.Work();
            }

            foreach (var key in bars.Keys)
            {
                bars[key].SetBarValue(resources[key], 500);
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
    money,
}