using System;
using System.Threading.Tasks;
using UnityEngine;

public class MapDisplayer : IDisposable
{
    public Transform MapRoot;
    public string tilePref = "Tile";
    public float HexSize = 1f; // от центра до вершины
    public float HexY = 1f;

    private MapGenContext genContext;
    private Factory factory = new();

    public MapDisplayer(MapGenContext genContext)
    {
        this.genContext = genContext;
    }

    public void DisplayMap()
    {
        _ = DisplayMapAsync();
    }


    public async Task DisplayMapAsync()
    {
        await factory.LoadAsync(tilePref);

        foreach (var tileData in genContext.TilesData.Values)
        {
            var worldPos = CubeToWorld(tileData.CubeCoord);
            worldPos.y = (int)tileData.BiomeType * HexY;

            var obj = factory.Instantiate(tilePref, worldPos, Quaternion.identity, MapRoot);

            var component = obj.GetComponent<Tile>();
            component.TileData = tileData;

            await Task.Yield();
        }
    }

    private Vector3 CubeToWorld(Vector3Int cube) // pointy-top
    {
        float x = HexSize * Mathf.Sqrt(3f) * (cube.x + cube.z * 0.5f);
        float z = HexSize * 1.5f * cube.z;

        return new Vector3(x, 0f, z);
    }


    public void Dispose()
    {
        factory.Dispose();
    }
}
