using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Building : Entity
{
    [Space]
    public BuildingComponent[] components;

    public virtual void Init()
    {
        tilesInRadius = GetTilesInRadius(tile.tileData.cubeCoord, radius);
    }


    public virtual void Active()
    {
        foreach (var component in components)
        {
            component.owner = this;
            component.Active();
        }

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOScale(new Vector3(0.95f, 1.1f, 0.95f), 0.25f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
    }

    public virtual void Delete()
    {
        Destroy(gameObject);
    }


    public IEnumerator OnSpawn()
    {
        Camera cam = Camera.main;
        LayerMask rayLayer = LayerMask.GetMask("Ground");
        while (true)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 30, rayLayer))
            {
                transform.position = hit.transform.position;
            }

            yield return null;
        }
    }

    public bool TryPlace(Tile newTile)
    {
        foreach (var offset in shape)
        {
            Vector3Int tilePos = newTile.tileData.cubeCoord + offset;
            if (MapGenerator.Instance.TileMap.TryGetValue(tilePos, out Tile tileOnMap) == false)
            {
                return false;
            }

            if (tileOnMap.tileData.isTaken != null)
            {
                return false;
            }

            if (tileOnMap.tileData.tileType != newTile.tileData.tileType)
            {
                return false;
            }
        }

        tile = newTile;
        foreach (var offset in shape)
        {
            Vector3Int tilePos = newTile.tileData.cubeCoord + offset;
            MapGenerator.Instance.TileMap[tilePos].tileData.isTaken = this;
        }

        StopAllCoroutines();
        Init();
        Active();
        return true;
    }
}
