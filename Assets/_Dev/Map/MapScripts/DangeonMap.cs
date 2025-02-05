using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DangeonMap : MonoBehaviour
{
    [SerializeField] private Transform[] mapPoints;
    [SerializeField] private int height;
    //[SerializeField] private int width;

    public System.Random random;
    private int seed;
    private List<int> dangeonIndexs = new List<int>();

    private void NewGenerate(MyEvent.OnEntryMap _)
    {
        seed = DateTime.Now.Millisecond;
        random = new System.Random(seed);

        for (byte i = 0; i < height; i++)
        {
            int mapIndex = random.Next(0, Content.instance.maps.containers.Length);
            dangeonIndexs.Add(mapIndex);
        }
    }


    private void GenerateMap(EventArgs _)
    {
        for (byte i = 0; i < height; i++)
        {
            var mapAdress = Content.instance.maps.containers[dangeonIndexs[i]];
            var handle = Addressables.LoadAssetAsync<GameObject>(mapAdress);
            handle.WaitForCompletion();

            GameObject newMap = Instantiate(handle.Result);
            newMap.transform.SetParent(mapPoints[i], false);
            newMap.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
        }
    }

    public DangeonData GetRandomDangeon()
    {
        DangeonData dangeon = new DangeonData();

        dangeon.mapIndex = random.Next(0, Content.instance.maps.containers.Length);
        dangeon.waveIndex = random.Next(0, Content.instance.waves.containers.Length);

        List<int> emptyPos = new List<int>();

        for (byte i = 0; i < 4; i++)
        {
            emptyPos.Add(i);
        }

        for (byte i = 0; i < Content.instance.waves.containers[dangeon.waveIndex].enemys.Length; i++)
        {
            int newPos = random.Next(0, emptyPos.Count);
            dangeon.enemysPos[i] = emptyPos[newPos];
            emptyPos.Remove(newPos);
        }

        return dangeon;
    }


    private void Save(MyEvent.OnSave _)
    {
        SaveDangeonMap saveDangeonMap = new SaveDangeonMap();
        saveDangeonMap.seed = seed;
        saveDangeonMap.dangeonIndexs = dangeonIndexs.ToArray();

        SaveSystem.gameData.saveDangeonMap = saveDangeonMap;
    }

    private void Load(MyEvent.OnLoad _)
    {
        seed = SaveSystem.gameData.saveDangeonMap.seed;
        random = new System.Random(seed);
        
        dangeonIndexs = SaveSystem.gameData.saveDangeonMap.dangeonIndexs.ToList();
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnSave>(Save);

        EventBus.Add<MyEvent.OnLoad>(Load, 1);
        EventBus.Add<MyEvent.OnLoad>(GenerateMap);

        EventBus.Add<MyEvent.OnEntryMap>(NewGenerate);
        EventBus.Add<MyEvent.OnEntryMap>(GenerateMap);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnSave>(Save);

        EventBus.Remove<MyEvent.OnLoad>(Load);
        EventBus.Remove<MyEvent.OnLoad>(GenerateMap);

        EventBus.Remove<MyEvent.OnEntryMap>(NewGenerate);
        EventBus.Remove<MyEvent.OnEntryMap>(GenerateMap);
    }
}

[System.Serializable]
public struct SaveDangeonMap
{
    public int seed;
    public int[] dangeonIndexs;
}

[System.Serializable]
public struct DangeonData
{
    public int mapIndex;
    public int waveIndex;

    public int[] enemysPos;
}
