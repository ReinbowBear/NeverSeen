using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public Weapon[] weapons = new Weapon[4];

    public void EquipWeapon(Weapon newWeapon, byte slot)
    {
        if (weapons[slot] != null)
        {
            Address.DestroyAsset(weapons[slot].gameObject);
        }

        weapons[slot] = newWeapon;
        newWeapon.transform.SetParent(transform, false);

        InventoryUI.instance.ShowItems(this);
    }
}
