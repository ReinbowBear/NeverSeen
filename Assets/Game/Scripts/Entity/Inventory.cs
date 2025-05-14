using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private AssetReference[] startedItems;

    [HideInInspector] public Ability[] weapons = new Ability[3];

    void Start()
    {
        for (byte i = 0; i < startedItems.Length; i++)
        {
            StartCoroutine(AddWeapon(startedItems[i], i));
        }
    }


    private IEnumerator AddWeapon(AssetReference weaponToLoad, byte slot)
    {
        var handle = Addressables.InstantiateAsync(weaponToLoad, weaponPoint, false);
        yield return handle;

        var release = handle.Result.AddComponent<ReleaseOnDestroy>();
        release.handle = handle;

        if (weapons[slot] != null)
        {
            Destroy(weapons[slot].gameObject);
        }

        weapons[slot] = handle.Result.GetComponent<Ability>();

        InventoryUI.instance.ShowItems(this);
    }
}
