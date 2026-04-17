using System;
using System.Threading.Tasks;
using UnityEngine;

public class ProxyMapGenerator : MonoBehaviour, IEventListener
{
    private World world;

    private TileMap tileMap;
    private Factory factory;
    private RandomService random;

    public MapGenerator mapGenerator;

    public ProxyMapGenerator()
    {
        mapGenerator = new(tileMap, factory, random);
    }

    public void SetEvents(World world)
    {
        //eventWorld.AddListener(GenerateMap, Events.SceneEvents.EnterScene);
    }


    public void GenerateMap()
    {
        _ = GenerateMap2();
    }

    public async Task GenerateMap2()
    {
        try
        {
            await mapGenerator.GenerateNewMap();
        }
        catch (Exception exception)
        {
            Debug.LogError(exception);
        }
        //eventWorld.Invoke(tileMap, Events.ObjectEvents.Spawn);
    }
}
