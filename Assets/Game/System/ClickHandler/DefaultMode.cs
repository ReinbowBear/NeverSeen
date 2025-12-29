using UnityEngine;

public class DefaultMode : IViewMode
{
    private LayerMask rayLayer;
    private EntityOld ChosenEntity;

    public DefaultMode(LayerMask  rayLayer)
    {
        this.rayLayer = rayLayer;
    }


    public LayerMask GetRayLayer()
    {
        return rayLayer;
    }


    public void LeftClick(RaycastHit hit)
    {
        RightClick();

        var tile = hit.transform.gameObject.GetComponent<Tile>();
        ChosenEntity = tile.tileData.IsTaken;
        ChosenEntity?.Selected(true);
    }

    public void RightClick()
    {
        if (ChosenEntity != null)
        {
            ChosenEntity.Selected(false);
            ChosenEntity = null;
        }
    }
}
