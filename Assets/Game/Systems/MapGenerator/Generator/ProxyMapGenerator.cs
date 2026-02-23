using System;
using UnityEngine;

public class ProxyMapGenerator : MonoBehaviour, IInitializable, IEventListener, IDisposable
{
    public Transform MapRoot;

    private TileMap mapData;
    private MapGenContext genContext = new();
    private MyRandom random;

    public MapGenData mapGenData;

    public MapGenerator mapGenerator;
    public MapDisplayer mapDisplayer;

    public void Init()
    {
        mapGenerator = new(mapGenData, genContext, random);
        mapDisplayer = new(genContext)
        {
            MapRoot = MapRoot
        };
    }

    public void SetEvents(EventWorld eventWorld)
    {
        eventWorld.AddListener(this, mapGenerator.GenerateMap, Events.SceneEvents.EnterScene);
        eventWorld.AddListener(this, mapDisplayer.DisplayMap, Events.SceneEvents.EnterScene);
    }


    public void Dispose()
    {
        mapDisplayer.Dispose();
    }
}
