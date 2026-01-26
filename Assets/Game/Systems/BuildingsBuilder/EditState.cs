using UnityEngine;

public class EditState : IViewMode, IState
{
    public LayerMask rayLayer;
    public BuilderData builderData;

    public EditState(BuilderData builderData, LayerMask  rayLayer)
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
        if (builderData.BuildingInHand == null)
        {
            //clickHandler.SetMode(0);
            return;
        }

        Tile tile = hit.transform.gameObject.GetComponent<Tile>();
        TryPlace(tile);
    }

    public void RightClick()
    {
        if (builderData.BuildingInHand == null) return;

        GameObject.Destroy(builderData.BuildingInHand.gameObject);
        builderData.BuildingInHand = null;
    }

    private void TryPlace(Tile newTile)
    {
        if (tileMap.IsCanPlace(newTile, builderData.BuildingInHand.Shape))
        {
            foreach (var offset in Shape.Shapes[builderData.BuildingInHand.Shape])
            {
                Vector3Int tilePos = newTile.tileData.CubeCoord + offset;
                tileMap.Tiles[tilePos].tileData.IsTaken = builderData.BuildingInHand;
            }
            builderData.BuildingInHand = null;

            //if(entity.TryGetComponent<Initialazer>(out var component)) component.Initialize();

            //Tween.Spawn(entity.transform);
        }
        else
        {
            //Tween.Shake(ChosenEntity.transform);
        }
    }
}
