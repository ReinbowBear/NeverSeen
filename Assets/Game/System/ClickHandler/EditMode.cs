using System;
using UnityEngine;

public class EditMode : IViewMode
{
    public Action OnNewBuilding;
    public Building Building { get; private set; }

    private LayerMask rayLayer;
    private GameMapData mapData;

    public EditMode(GameData gameData)
    {
        this.mapData = gameData.GameMap;
    }

    public void Init(LayerMask rayLayer)
    {
        this.rayLayer = rayLayer;
    }


    public LayerMask GetRayLayer()
    {
        return rayLayer;
    }

    public void LeftClick(RaycastHit hit)
    {
        if (Building == null) return;

        Tile tile = hit.transform.gameObject.GetComponent<Tile>();
        TryPlace(tile);
    }

    public void RightClick()
    {
        if (Building == null) return;

        GameObject.Destroy(Building.gameObject);
        Building = null;
    }


    public void SetBuilding(Building newBuilding)
    {
        Building = newBuilding;
        OnNewBuilding?.Invoke();
    }


    private void TryPlace(Tile newTile)
    {
        if (mapData.CanPlaceBuilding(newTile, Building.Stats.Shape))
        {
            foreach (var offset in Shape.Shapes[Building.Stats.Shape])
            {
                Vector3Int tilePos = newTile.tileData.CubeCoord + offset;
                mapData.TileMap[tilePos].tileData.IsTaken = Building;
            }

            Building.Init(newTile, mapData.GetTilesInRadius(newTile.tileData.CubeCoord, Building.Stats.Radius));
            Tween.Spawn(Building.transform);
            Building = null;
        }
        else
        {
            Tween.Shake(Building.transform);
        }
    }
}
