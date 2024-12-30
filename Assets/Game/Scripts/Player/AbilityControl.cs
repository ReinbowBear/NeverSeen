using UnityEngine;

public class AbilityControl : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Transform abilityPoint;
    [Space]
    public BarChange mpBar;


    private Ability[] abilitys;

    void Start()
    {
        abilitys = new Ability[character.inventory.abilitys.Length];
    }


    private async void GetAbility(MyEvent.OnEntryBattle _)
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
        EventBus.Add<MyEvent.OnEntryBattle>(GetAbility);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEntryBattle>(GetAbility);
    }
}
