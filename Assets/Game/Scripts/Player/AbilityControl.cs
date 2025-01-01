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
        abilitys = new Ability[character.inventory.abilitySlots.Length];
    }


    private async void InitAbility(MyEvent.OnStartBattle _)
    {
        for (byte i = 0; i < character.inventory.abilitySlots.Length; i++)
        {
            if (character.inventory.abilitySlots[i].GetItem() != null)
            {
                AbilityContainer newAbility = character.inventory.abilitySlots[i].GetItem().container as AbilityContainer;
                abilitys[i] = await character.abilityFactory.GetAbility(newAbility.stats);
            }
        }

        for (byte i = 0; i < abilitys.Length; i++)
        {
            abilitys[i].transform.SetParent(abilityPoint, false);
            abilitys[i].gameObject.SetActive(false);
        }
    }


    public void ChoseAbility(byte index)
    {
        Debug.Log("не готово ещё тут");
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnStartBattle>(InitAbility);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnStartBattle>(InitAbility);
    }
}
