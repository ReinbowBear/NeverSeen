using System.Collections;
using UnityEngine;

public class Ability : Applicable
{
    [HideInInspector] public Character character;

    [SerializeField] protected AbilitySO abilitySO;
    [HideInInspector] public AbilitySO stats;

    protected bool isColldown;
    protected float cooldown;

    protected virtual void Awake()
    {
        stats = Instantiate(abilitySO);
    }


    protected override IEnumerator Reload()
    {
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
        
        if (character.entityManager.enemys[0] != null)
        {
            character.entityManager.enemys[0].health.TakeDamage(5);
            yield return null;
        }
    }
}
