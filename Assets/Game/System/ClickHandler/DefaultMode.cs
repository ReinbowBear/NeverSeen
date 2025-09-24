using UnityEngine;

public class DefaultMode : IViewMode
{
    private LayerMask rayLayer;
    private TileMapData mapData;

    public DefaultMode(LayerMask  rayLayer, TileMapData mapData)
    {
        this.rayLayer = rayLayer;
        this.mapData = mapData;
    }


    public LayerMask GetRayLayer()
    {
        return rayLayer;
    }


    public void LeftClick(RaycastHit hit)
    {
        RightClick();

        mapData.CurrentEntity = hit.transform.gameObject.GetComponent<Entity>();
        mapData.CurrentEntity.Selected(true);
    }

    public void RightClick()
    {
        if (mapData.CurrentEntity != null)
        {
            mapData.CurrentEntity.Selected(false);
            mapData.CurrentEntity = null;
        }
    }
}
