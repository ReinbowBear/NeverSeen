using UnityEngine;

public class DefaultMode : IViewMode
{
    private LayerMask rayLayer;
    private Entity SelectedEntity;

    public DefaultMode(LayerMask rayLayer)
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

        SelectedEntity = hit.transform.gameObject.GetComponent<Entity>();
        SelectedEntity.Selected(true);
    }

    public void RightClick()
    {
        if (SelectedEntity != null)
        {
            SelectedEntity.Selected(false);
            SelectedEntity = null;
        }
    }
}
