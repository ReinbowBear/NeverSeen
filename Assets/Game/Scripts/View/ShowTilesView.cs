using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShowTilesView : MonoBehaviour
{
    public int Radius;

    private List<Tile> tilesInRadius = new();
    private TileMapData mapData;

    [Inject]
    public void Construct(TileMapData mapData)
    {
        this.mapData = mapData;
    }


    public void ShowTiles()
    {
        SetTileslight(false);
        var tile = mapData.GetTileFromCord(transform.position);
        var cord = tile.tileData.CubeCoord;

        tilesInRadius = mapData.GetTilesInRadius(cord, Radius);
        SetTileslight(true);
    }

    public void HideTiles() // подразумевается что тайлы светятся
    {
        SetTileslight(false);
    }


    private void SetTileslight(bool isActive)
    {
        foreach (var tile in tilesInRadius)
        {
            tile.SetBacklight(isActive);
        }
    }


    void OnDestroy()
    {
        SetTileslight(false);
    }
}
