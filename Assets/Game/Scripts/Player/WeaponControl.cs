using System.Collections;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Transform weaponPoint;
    [Space]
    public BarChange wpBar;

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

        if (myCoroutine != null)
        {
            StopCoroutine(myCoroutine);
        }
        myCoroutine = StartCoroutine(Attaking(weaponID));
    }

    public IEnumerator Attaking(byte weaponID)
    {
        while (myCoroutine != null)
        {
            weapons[weaponID].Activate();
            yield return StartCoroutine(weapons[weaponID].Reload());
        }
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntryBattle>(GetWeapon);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEntryBattle>(GetWeapon);
    }
}
