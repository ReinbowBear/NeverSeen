using System;
using UnityEngine;

public class EditMode : IState, IViewMode
{
    private LayerMask rayLayer;
    private GameMapData gameMap;

    public EditMode(int rayLayer, GameData gameData)
    {
        this.rayLayer = new LayerMask { value = rayLayer };
        this.gameMap = gameData.GameMap;
    }


    public void Enter() { }
    public void Exit() { }


    public LayerMask GetRayLayer()
    {
        return rayLayer;
    }

    public void LeftClick(RaycastHit hit)
    {
        if (gameMap.CurrentBuilding == null) return;

        Tile tile = hit.transform.gameObject.GetComponent<Tile>();
        TryPlace(tile);
    }

    public void RightClick()
    {
        if (gameMap.CurrentBuilding == null) return;

        GameObject.Destroy(gameMap.CurrentBuilding.gameObject);
        gameMap.CurrentBuilding = null;
    }

    private void TryPlace(Tile newTile)
    {
        var building = gameMap.CurrentBuilding;
        if (gameMap.CanPlace(newTile, building.Stats.Shape))
        {
            foreach (var offset in Shape.Shapes[building.Stats.Shape])
            {
                Vector3Int tilePos = newTile.tileData.CubeCoord + offset;
                gameMap.TileMap[tilePos].tileData.IsTaken = building;
            }

            building.Init(newTile, gameMap.GetTilesInRadius(newTile.tileData.CubeCoord, building.Stats.Radius));
            Tween.Spawn(building.transform);
            building = null;
        }
        else
        {
            Tween.Shake(building.transform);
        }
    }
}
