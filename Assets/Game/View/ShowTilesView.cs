using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShowTilesView : MonoBehaviour
{
    public int Radius;

    private List<Tile> tilesInRadius = new();
    private TileMap mapData;

    [Inject]
    public void Construct(TileMap mapData)
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

    public void HideTiles()
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


    void OnDisable()
    {
        SetTileslight(false);
    }
}
