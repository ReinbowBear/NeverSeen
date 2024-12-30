using System.Collections;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [HideInInspector] public Character character;

    [SerializeField] protected AbilitySO abilitySO;
    [HideInInspector] public AbilitySO stats;

    protected float cooldown;
    [HideInInspector] public Coroutine AttackCorutine;

    protected virtual void Awake()
    {
        stats = Instantiate(abilitySO);
    }


    public virtual void Prepare()
    {
        if (cooldown >= stats.reloadTime)
        {
            character.combatManager.AddAction(this);
        }
    }

    public virtual void Activate()
    {
        Enemy enemy = GetEnemy(0);
        AttackCorutine = StartCoroutine(GetAttack(enemy));
        StartCoroutine(GetReload());
    }


    protected virtual Enemy GetEnemy(int index)
    {
        if (character.battleMap.enemyPoints[index].childCount > 0)
        {
            Enemy enemy = character.battleMap.enemyPoints[index].GetComponentInChildren<Enemy>();
            return enemy;
        }
        return null;
    }

    protected virtual IEnumerator GetAttack(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.health.TakeDamage(stats.damage);
        }
        yield return null;
        AttackCorutine = null;
    }

    protected virtual IEnumerator GetReload()
    {
        cooldown = 0;
        while (cooldown !>= stats.reloadTime)
        {
            character.abilityControl.mpBar.ChangeBar(cooldown, stats.reloadTime);

            cooldown += Time.deltaTime;
            yield return null;
        }
    }


    public virtual void False()
    {
        StopAllCoroutines();
    }
}
