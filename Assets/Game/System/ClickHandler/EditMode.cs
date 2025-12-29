using UnityEngine;

public class EditMode : IViewMode
{
    private LayerMask rayLayer;
    private TileMap tileMap;

    private ClickHandler clickHandler;
    private EntityOld ChosenEntity;

    public EditMode(LayerMask  rayLayer, ClickHandler clickHandler, TileMap tileMap)
    {
        this.rayLayer = rayLayer;
        this.clickHandler = clickHandler;
        this.tileMap = tileMap;
    }


    public LayerMask GetRayLayer()
    {
        return rayLayer;
    }


    public void LeftClick(RaycastHit hit)
    {
        if (ChosenEntity == null)
        {
            clickHandler.SetMode(0);
            return;
        }

        Tile tile = hit.transform.gameObject.GetComponent<Tile>();
        TryPlace(tile);
    }

    public void RightClick()
    {
        if (ChosenEntity == null) return;

        GameObject.Destroy(ChosenEntity.gameObject);
        ChosenEntity = null;
    }

    private void TryPlace(Tile newTile)
    {
        if (tileMap.IsCanPlace(newTile, ChosenEntity.Shape))
        {
            foreach (var offset in Shape.Shapes[ChosenEntity.Shape])
            {
                Vector3Int tilePos = newTile.tileData.CubeCoord + offset;
                tileMap.Tiles[tilePos].tileData.IsTaken = ChosenEntity;
            }
            ChosenEntity = null;

            //if(entity.TryGetComponent<Initialazer>(out var component)) component.Initialize();

            //Tween.Spawn(entity.transform);
        }
        else
        {
            Tween.Shake(ChosenEntity.transform);
        }
    }
}
