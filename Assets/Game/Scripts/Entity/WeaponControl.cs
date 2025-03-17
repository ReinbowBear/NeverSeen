using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public Inventory inventory;
    
    private int previousWeapon;

    public void DoAttack(int index)
    {
        Weapon weapon = inventory.weapons[index];

        if (weapon == null || weapon.corutine != null)
        {
            return;
        }

        if (inventory.weapons[previousWeapon].corutine != null) // кансел атаки другой способностью
        {
            inventory.weapons[previousWeapon].FalseAttack();
        }

        previousWeapon = index;
        weapon.Attack();
    }
}
