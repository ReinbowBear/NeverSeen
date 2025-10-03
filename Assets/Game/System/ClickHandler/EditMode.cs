using UnityEngine;

public class EditMode : IViewMode
{
    private LayerMask rayLayer;
    private TileMap tileMap;
    private World world;

    private ClickHandler clickHandler;

    public EditMode(LayerMask  rayLayer, ClickHandler clickHandler, TileMap tileMap, World world)
    {
        this.rayLayer = rayLayer;
        this.clickHandler = clickHandler;
        this.tileMap = tileMap;
        this.world = world;
    }


    public LayerMask GetRayLayer()
    {
        return rayLayer;
    }


    public void LeftClick(RaycastHit hit)
    {
        if (world.ChosenEntity == null)
        {
            clickHandler.SetMode(0);
            return;
        }

        Tile tile = hit.transform.gameObject.GetComponent<Tile>();
        TryPlace(tile);
    }

    public void RightClick()
    {
        if (world.ChosenEntity == null) return;

        GameObject.Destroy(world.ChosenEntity.gameObject);
        world.ChosenEntity = null;
    }

    private void TryPlace(Tile newTile)
    {
        var entity = world.ChosenEntity;
        if (tileMap.IsCanPlace(newTile, entity.Stats.Shape))
        {
            foreach (var offset in Shape.Shapes[entity.Stats.Shape])
            {
                Vector3Int tilePos = newTile.tileData.CubeCoord + offset;
                tileMap.Tiles[tilePos].tileData.IsTaken = entity;
            }
            world.ChosenEntity = null;
            entity.GetComponent<BuildingAI>().Initialize();
            //Tween.Spawn(entity.transform);
        }
        else
        {
            Tween.Shake(entity.transform);
        }
    }
}
