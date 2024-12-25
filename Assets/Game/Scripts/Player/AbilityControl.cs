using UnityEngine;

public class AbilityControl : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private BarChange mpBar;
    [Space]
    [SerializeField] private Transform abilityPoint;

    private Ability[] abilitys;

    void Awake()
    {
        abilitys = new Ability[character.inventory.abilitys.Length];
    }


    private async void GetWeapon(MyEvent.OnEntryBattle _)
    {
        for (byte i = 0; i < character.inventory.abilitys.Length; i++)
        {
            if (character.inventory.abilitys[i] != null)
            {
                GameObject newAbility = await Content.GetAsset(character.inventory.abilitys[i].GetItem().container.prefab, abilityPoint);
                abilitys[i] = newAbility.GetComponent<Ability>();
                abilitys[i].character = character;
            }
        }

        for (byte i = 0; i < abilitys.Length; i++)
        {
            abilitys[i].gameObject.SetActive(false);
        }
    }


    public void ChoseAbility(byte index)
    {
        Debug.Log("не готово ещё тут");
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
