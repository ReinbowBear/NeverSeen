using DG.Tweening;
using UnityEngine;

public class LootUI : MonoBehaviour
{
    public Transform[] LootSlots;

    private async void ShowItems()
    {
        SaveLoot loot = SaveSystem.gameData.saveLoot;

        for (byte i = 0; i < LootSlots.Length; i++)
        {
            AbilitySO abilitySO = Content.instance.abilityDataBase.GetItemByName(loot.items[i]) as AbilitySO;
            Item newItem = await ItemFactory.GetItem(abilitySO);

            newItem.transform.SetParent(LootSlots[i].transform, false);
            newItem.Init(abilitySO);
            MoveToPos(newItem.transform);
        }
    }

    public void MoveToPos(Transform newItemTrans)
    {
        newItemTrans.localScale = new Vector3(0.8f, 1.4f, 0.8f);
        DOTween.Sequence()
            .SetLink(newItemTrans.gameObject)
            .Append(newItemTrans.DOMove(new Vector3(newItemTrans.position.x, 8, newItemTrans.position.z), 0.75f).From()).SetEase(Ease.Linear)
            .Append(newItemTrans.DOScale(new Vector3(1.2f, 0.8f, 1.2f), 0.15f)).SetEase(Ease.Linear)
            .Append(newItemTrans.DOScale(new Vector3(1, 1, 1), 0.25f));
    }
}

[System.Serializable]
public struct SaveLoot
{
    public string[] items;
}