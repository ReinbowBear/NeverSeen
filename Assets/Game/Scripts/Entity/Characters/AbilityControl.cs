using UnityEngine;

public class AbilityControl : MonoBehaviour
{
    [SerializeField] private Entity entity;
    [SerializeField] private Transform abilityPoint;
    [Space]
    public BarChange mpBar;

    [HideInInspector] public Ability[] abilitys;

    void Awake()
    {
        abilitys = new Ability[entity.inventory.abilitys.Length];
    }


    public void AddAbility(Ability newAbility, byte index)
    {
        abilitys[index] = newAbility;
        abilitys[index].character = entity;

        abilitys[index].transform.SetParent(abilityPoint);
        abilitys[index].gameObject.SetActive(false);
    }

    public void RemoveAbility(byte index)
    {
        Address.DestroyAsset(abilitys[index].gameObject);
        abilitys[index] = null;
    }


    public void ChoseAbility(byte index)
    {
        abilitys[index].Prepare();
    }
}
