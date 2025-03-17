using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class StartItems : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private AssetReference[] startedItems;

    void Awake()
    {
        StartCoroutine(AddItems());
    }


    private IEnumerator AddItems()
    {
        for (byte i = 0; i < startedItems.Length; i++)
        {
            var handle = Address.GetAsset(startedItems[i]);
            yield return new WaitUntil(() => handle.IsCompleted);
            Weapon Item = handle.Result.GetComponent<Weapon>();

            inventory.EquipWeapon(Item ,i);
        }
    }
}
