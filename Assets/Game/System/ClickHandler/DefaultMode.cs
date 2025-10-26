using UnityEngine;

public class DefaultMode : IViewMode
{
    private LayerMask rayLayer;
    private EntityRegistry world;

    public DefaultMode(LayerMask  rayLayer, EntityRegistry world)
    {
        this.rayLayer = rayLayer;
        this.world = world;
    }


    public LayerMask GetRayLayer()
    {
        return rayLayer;
    }


    public void LeftClick(RaycastHit hit)
    {
        RightClick();

        var tile = hit.transform.gameObject.GetComponent<Tile>();
        world.ChosenEntity = tile.tileData.IsTaken;
        world.ChosenEntity?.Selected(true);
    }

    public void RightClick()
    {
        if (world.ChosenEntity != null)
        {
            world.ChosenEntity.Selected(false);
            world.ChosenEntity = null;
        }
    }
}
