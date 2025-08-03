using DG.Tweening;
using UnityEngine;

public class Miner : BuildingAction
{
    [SerializeField] protected short miningValue;
    [SerializeField] protected short storageSize;

    [HideInInspector] public int storage;

    private Ore ore;

    public override void Init()
    {
        if (Owner.Tile.gameObject.TryGetComponent<Ore>(out Ore newOre))
        {
            ore = newOre;
        }
    }

    public override void Active()
    {
        if (ore == null) return;
        PlayerResource.Instance.AddBuilding(this);
    }

    public override void Deactive()
    {
        PlayerResource.Instance.RemoveBuilding(this);
    }


    public void Work() // БАГ работает даже без генератора
    {
        if (miningValue > ore.OreCapasity)
        {
            Debug.Log("недостаточно ресурсов"); // в руде может остатся чуть ресурсов но мы их не докопаем пока что
            Deactive();
            return;
        }

        if (storage + miningValue <= storageSize)
        {
            storage += miningValue;
        }

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOScale(new Vector3(0.95f, 1.1f, 0.95f), 0.25f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
    }
}
