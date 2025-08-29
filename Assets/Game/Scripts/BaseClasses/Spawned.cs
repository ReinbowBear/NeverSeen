using System;
using System.Collections;
using UnityEngine;

public abstract class Spawned : Entity // TilesInRadius и Tile заполняются в этом классе
{
    public Action OnSpawned;
    public GameMapState mapData;

    public IEnumerator OnSpawn()
    {
        var cam = Camera.main;
        var rayLayer = LayerMask.GetMask("Tile");

        Tile oldTile = null;
        Tile newTile;

        while (true)
        {
            Ray ray = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, 30, rayLayer)) yield return null;

            transform.position = hit.transform.position;
            newTile = hit.transform.gameObject.GetComponent<Tile>();

            if (newTile == oldTile) yield return null;

            foreach (var tile in tilesInRadius)
            {
                tile.SetBacklight(false);
            }

            tilesInRadius = mapData.GetTilesInRadius(newTile.tileData.CubeCoord, radius); // TilesInRadius

            foreach (var tile in tilesInRadius)
            {
                tile.SetBacklight(true);
            }
            oldTile = newTile;


            yield return null;
        }
    }

    public bool TryPlace(Tile newTile)
    {
        foreach (var offset in shape)
        {
            Vector3Int tilePos = newTile.tileData.CubeCoord + offset;

            if (mapData.TileMap.TryGetValue(tilePos, out Tile tileOnMap) == false) return false;

            if (tileOnMap.tileData.IsTaken != null) return false;

            if (tileOnMap.tileData.TileHeightType != newTile.tileData.TileHeightType) return false;
        }

        StopAllCoroutines();

        foreach (var offset in shape)
        {
            Vector3Int tilePos = newTile.tileData.CubeCoord + offset;
            mapData.TileMap[tilePos].tileData.IsTaken = this;
        }

        foreach (var tile in tilesInRadius) // гасим подсветку радиуса когда таскал здание на карту
        {
            tile.SetBacklight(false);
        }

        tile = newTile; // Tile
        OnSpawned?.Invoke();
        return true;
    }
}
