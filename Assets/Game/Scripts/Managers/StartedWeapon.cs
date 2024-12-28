using System;
using UnityEngine;

public class StartedWeapon : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject itemPrefab;
    [Space]
    [SerializeField] private WeaponDataBase gameWeapons;
    [SerializeField] private AbilityDataBase gameAbilitys;
    [SerializeField] private EquipmentDataBase gameEquipment;
    [SerializeField] private ArmorDataBase gameArmors;

    private System.Random random;

    void Awake()
    {
        random = new System.Random(DateTime.Now.Millisecond);

        AddWeapon(random.Next(0, gameWeapons.containers.Length));
        for (byte i = 0; i < inventory.abilitys.Length; i++)
        {
            AddAbility(random.Next(0, gameAbilitys.containers.Length));
        }
    }


    private void AddWeapon(int index)
    {
        for (byte i = 0; i < inventory.weapons.Length; i++)
        {
            if (inventory.weapons[i].GetItem() == null)
            {
                Item newItem = Instantiate(itemPrefab, inventory.weapons[i].transform).GetComponent<Item>();
                newItem.Init(gameWeapons.containers[index]);

                break;
            }
        }
    }

    private void AddAbility(int index)
    {
        for (byte i = 0; i < inventory.abilitys.Length; i++)
        {
            if (inventory.abilitys[i].GetItem() == null)
            {
                Item newItem = Instantiate(itemPrefab, inventory.abilitys[i].transform).GetComponent<Item>();
                newItem.Init(gameAbilitys.containers[index]);

                break;
            }
        }
    }

    private void AddRing(int index)
    {
        for (byte i = 0; i < inventory.rings.Length; i++)
        {
            if (inventory.rings[i].GetItem() == null)
            {
                Item newItem = Instantiate(itemPrefab, inventory.rings[i].transform).GetComponent<Item>();
                newItem.Init(gameEquipment.containers[index]);

                break;
            }
        }
    }

    private void AddWArmor(int index)
    {
        if (inventory.armor.GetItem() == null)
        {
            Item newItem = Instantiate(itemPrefab, inventory.armor.transform).GetComponent<Item>();
            newItem.Init(gameArmors.containers[index]);
        }
        
    }
}
