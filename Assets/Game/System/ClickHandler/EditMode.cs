using UnityEngine;

public class EditMode : IViewMode
{
    private LayerMask rayLayer;
    private TileMapData mapData;

    private ClickHandler clickHandler;

    public EditMode(LayerMask  rayLayer, ClickHandler clickHandler, TileMapData mapData)
    {
        this.rayLayer = rayLayer;
        this.clickHandler = clickHandler;
        this.mapData = mapData;
    }


    public LayerMask GetRayLayer()
    {
        return rayLayer;
    }


    public void LeftClick(RaycastHit hit)
    {
        if (mapData.CurrentEntity == null)
        {
            clickHandler.SetMode<DefaultMode>();
            return;
        }

        Tile tile = hit.transform.gameObject.GetComponent<Tile>();
        TryPlace(tile);
    }

    public void RightClick()
    {
        if (mapData.CurrentEntity == null) return;

        GameObject.Destroy(mapData.CurrentEntity.gameObject);
        mapData.CurrentEntity = null;
    }

    private void TryPlace(Tile newTile)
    {
        var entity = mapData.CurrentEntity;
        if (mapData.CanPlace(newTile, entity.Stats.Shape))
        {
            foreach (var offset in Shape.Shapes[entity.Stats.Shape])
            {
                Vector3Int tilePos = newTile.tileData.CubeCoord + offset;
                mapData.TileMap[tilePos].tileData.IsTaken = entity;
            }
            //entity.transform.position = newTile.transform.position; // позиция объекта устанавливается через MouseFollowView
            mapData.CurrentEntity = null;

            Tween.Spawn(entity.transform);
            Debug.Log("тут ещё активация здания походу должна быть");
        }
        else
        {
            Tween.Shake(entity.transform);
        }
    }
}
