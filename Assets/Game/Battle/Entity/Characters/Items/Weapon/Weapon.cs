using System.Collections;
using UnityEngine;

public class Weapon : Applicable
{
    [HideInInspector] public Character character;

    //[SerializeField] public Effect effect;
    [SerializeField] protected WeaponSO weaponSO;
    [HideInInspector] public WeaponSO stats;
    protected bool isColldown;
    protected float cooldown;

    protected virtual void Awake()
    {
        stats = Instantiate(weaponSO);
    }


    protected override IEnumerator Reload()
    {
        isColldown = true;
        cooldown = 0;
        while (cooldown != stats.reloadTime)
        {
            cooldown += Time.deltaTime;
            yield return null;
        }

        isColldown = false;
    }

    public override void Activate()
    {
        if (isColldown != true)
        {
            character.combatManager.AddAction(this);
        }
    }

    public override IEnumerator Attacking()
    {
        StartCoroutine(Reload());

        for (byte i = 0; i < character.entityManager.enemys.Length; i++)
        {
            if (character.entityManager.enemys[i] != null && stats.targets[i] == true)
            {
                character.entityManager.enemys[i].health.TakeDamage(stats.damage);
                yield return null;
            }
        }
    }
}
