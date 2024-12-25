using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private BarChange wpBar;
    [Space]
    [SerializeField] private Transform weaponPoint;

    private Coroutine myCoroutine;
    private Weapon[] weapons;
    private byte weaponID;

    void Awake()
    {
        weapons = new Weapon[character.inventory.weapons.Length];
    }


    private async void GetWeapon(MyEvent.OnEntryBattle _)
    {
        for (byte i = 0; i < character.inventory.weapons.Length; i++)
        {
            if (character.inventory.weapons[i] != null)
            {
                GameObject newWeapon = await Content.GetAsset(character.inventory.weapons[i].GetItem().container.prefab, weaponPoint);
                weapons[i] = newWeapon.GetComponent<Weapon>();
                weapons[i].character = character;
            }
        }

        for (byte i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }

        SetWeapon(0);
    }

    private void SweepWeapon()
    {
        weaponID++;

        if (weaponID > weapons.Length)
        {
            weaponID = 0;
        }

        if (weapons[weaponID] != null)
        {
            SetWeapon(weaponID);
        }
    }

    private void SetWeapon(byte weaponID)
    {
        weapons[weaponID].gameObject.SetActive(true);

        StartAttack(null);
    }

    public void StartAttack(MyEvent.OnStartBattle _)
    {
        Debug.Log("атаки ещё нету");
        //myCoroutine = currentWeapon.Activate();
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntryBattle>(GetWeapon);
        EventBus.Add<MyEvent.OnStartBattle>(StartAttack);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEntryBattle>(GetWeapon);
        EventBus.Remove<MyEvent.OnStartBattle>(StartAttack);
    }
}
