using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Building : Entity
{
    [Space]
    private ICondition[] Conditions;
    public BuildingAction[] Actions;
    public BuildingAction[] PassiveActions;

    public virtual void Init()
    {
        //TilesInRadius = GetTilesInRadius(Tile.tileData.CubeCoord, radius); // заполняется ещё когда таскаешь здание
        Conditions = GetComponents<ICondition>();

        foreach (var component in Actions)
        {
            component.Owner = this;
            component.Init();
        }

        foreach (var component in PassiveActions)
        {
            component.Owner = this;
            component.Init();
            component.Active(true);
        }

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOScale(new Vector3(0.95f, 1.1f, 0.95f), 0.25f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
    }


    public virtual void Active(bool isActive)
    {
        if (isActive)
        {
            foreach (var condition in Conditions)
            {
                if (!condition.IsConditionMet()) return;
            }
        }


        foreach (var action in Actions)
        {
            action.Active(isActive);
        }
    }


    public virtual void Delete()
    {
        base.Selected(false);
        foreach (var component in Actions)
        {
            component.Active(false);
            component.Delete();
        }
        foreach (var component in PassiveActions)
        {
            component.Active(false);
            component.Delete();
        }

        Destroy(gameObject);
    }


    public IEnumerator OnSpawn()
    {
        Camera cam = Camera.main;
        LayerMask rayLayer = LayerMask.GetMask("Tile");
        Tile hitTile = null;
        Tile newHitTile;
        while (true)
        {
            Ray ray = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 30, rayLayer))
            {
                transform.position = hit.transform.position;
                newHitTile = hit.transform.gameObject.GetComponent<Tile>();

                if (newHitTile != hitTile)
                {
                    foreach (var tile in TilesInRadius)
                    {
                        tile.ActiveTile(false);
                    }

                    TilesInRadius = GetTilesInRadius(newHitTile.tileData.CubeCoord, radius);

                    foreach (var tile in TilesInRadius)
                    {
                        tile.ActiveTile(true);
                    }
                    hitTile = newHitTile;
                }
            }

            yield return null;
        }
    }

    public bool TryPlace(Tile newTile)
    {
        foreach (var offset in shape)
        {
            Vector3Int tilePos = newTile.tileData.CubeCoord + offset;

            if (MapGenerator.Instance.TileMap.TryGetValue(tilePos, out Tile tileOnMap) == false) return false;

            if (tileOnMap.tileData.IsTaken != null) return false;

            if (tileOnMap.tileData.TileHeightType != newTile.tileData.TileHeightType) return false;
        }

        Tile = newTile;
        foreach (var offset in shape)
        {
            Vector3Int tilePos = newTile.tileData.CubeCoord + offset;
            MapGenerator.Instance.TileMap[tilePos].tileData.IsTaken = this;
        }

        StopAllCoroutines();
        foreach (var tile in TilesInRadius) // гасим подсветку радиуса когда таскал здание на карту
        {
            tile.ActiveTile(false);
        }
        Init();
        Active(true);
        return true;
    }
}
