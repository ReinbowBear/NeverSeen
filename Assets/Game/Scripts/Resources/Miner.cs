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

    public override void Active(bool isActive)
    {
        if (isActive && ore != null)
        {
            ResourceManager.Instance.AddBuilding(this);
        }
        else
        {
            ResourceManager.Instance.RemoveBuilding(this);
        }
    }


    public void Work() // БАГ работает даже без генератора
    {
        if (miningValue > ore.OreCapasity)
        {
            Debug.Log("недостаточно ресурсов"); // в руде может остатся чуть ресурсов но мы их не докопаем пока что
            Active(false);
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
