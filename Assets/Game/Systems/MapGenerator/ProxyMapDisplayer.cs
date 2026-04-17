using System.Threading.Tasks;
using UnityEngine;

public class ProxyMapDisplayer : MonoBehaviour, IEventListener
{
    private GameMap MapRoot;
    private Factory factory;

    public string tilePref = "Tile";
    public float HexY = 0.2f;


    public void SetEvents(World world)
    {
        //eventWorld.AddListener<TileMap>(DrawMap, Events.ObjectEvents.Spawn);
    }

    public void DrawMap(TileMap map)
    {
        _ = DrawMapAsync(map);
    }


    public async Task DrawMapAsync(TileMap map)
    {
        await factory.LoadAsync<GameObject>(tilePref);

        foreach (var tile in map.Tiles.Values)
        {
            var worldPos = map.CubeToWorld(tile.CubeCoord, Tile.HexSize);
            worldPos.y = (int)tile.Height * HexY;

            factory.Instantiate(tilePref, worldPos, Quaternion.identity, MapRoot.transform);
        }
    }
}
