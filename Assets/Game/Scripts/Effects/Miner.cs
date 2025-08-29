using UnityEngine;

public class Miner : IBehavior
{
    private short miningValue;
    private short storageSize;
    private Ore ore;

    public short storage;

    public Miner(short newMiningValue, short newStorageSize)
    {
        miningValue = newMiningValue;
        storageSize = newStorageSize;
    }


    public void SetActive(bool isActive)
    {
        if (ore == null)
        {
            Debug.Log("получаем руду и всё такое");
        }


        if (isActive && ore != null)
        {
            ResourceManager.Instance.AddBuilding(this);
        }
        else
        {
            ResourceManager.Instance.RemoveBuilding(this);
        }
    }


    public void Work()
    {
        if (miningValue > ore.OreCapasity)
        {
            Debug.Log("недостаточно ресурсов"); // в руде может остатся чуть ресурсов но мы их не докопаем пока что
            SetActive(false);
            return;
        }

        if (storage + miningValue <= storageSize)
        {
            storage += miningValue;
        }

        //TweenAnimation.Impact(transform);
    }
}
