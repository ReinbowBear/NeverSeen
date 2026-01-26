using UnityEngine;

public class ViewState : IViewMode, IState
{
    public LayerMask rayLayer;
    public BuilderData builderData;

    public ViewState(BuilderData builderData, LayerMask  rayLayer)
    {
        this.builderData = builderData;
        this.rayLayer = rayLayer;
    }

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }


    public void LeftClick(RaycastHit hit)
    {
        RightClick();

        var tile = hit.transform.gameObject.GetComponent<Tile>();
        builderData.BuildingInHand = tile.tileData.IsTaken;
        builderData.BuildingInHand?.Selected(true);
    }

    public void RightClick()
    {
        if (builderData.BuildingInHand != null)
        {
            builderData.BuildingInHand.Selected(false);
            builderData.BuildingInHand = null;
        }
    }
}
