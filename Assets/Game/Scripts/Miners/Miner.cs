using DG.Tweening;
using UnityEngine;

public class Miner : BuildingComponent
{
    [SerializeField] protected short miningValue;
    [SerializeField] protected short storageSize;

    [HideInInspector] public short storage;

    public override void Active()
    {
        PlayerResource.Instance.AddBuilding(this);
    }

    public virtual void Delete()
    {
        PlayerResource.Instance.RemoveBuilding(this);
    }


    public virtual void Work()
    {
        if (storage < storageSize)
        {
            storage += miningValue;
        }

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOScale(new Vector3(0.95f, 1.1f, 0.95f), 0.25f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
    }


    protected void OnDestroy()
    {
        Delete();
    }
}
