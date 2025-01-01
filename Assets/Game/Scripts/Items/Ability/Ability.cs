using System.Collections;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [HideInInspector] public Character character;

    [SerializeField] private AbilitySO abilitySO;
    [HideInInspector] public AbilitySO stats;

    [HideInInspector] public Target target;
    [HideInInspector] public Trigger trigger;
    [HideInInspector] public Effect effect;

    private float cooldown;

    void Awake()
    {
        stats = Instantiate(abilitySO);
    }


    public void Prepare()
    {
        if (cooldown >= stats.reloadTime)
        {
            character.combatManager.AddAction(this);
        }
    }

    public IEnumerator Activate()
    {
        StartCoroutine(Reload());

        Enemy[] enemys = target.GetTarget(character.battleMap);
        foreach (Enemy enemy in enemys)
        {
            if (enemy == null)
            {
                continue;
            }


            if (trigger != null && trigger.CheckTrigger() && effect != null)
            {
                effect.GetEffect(enemy);
            }
            
            enemy.health.TakeDamage(stats.damage);

            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator Reload()
    {
        cooldown = 0;
        while (cooldown !>= stats.reloadTime)
        {
            character.abilityControl.mpBar.ChangeBar(cooldown, stats.reloadTime);

            cooldown += Time.deltaTime;
            yield return null;
        }
    }


    public void False()
    {
        StopAllCoroutines();
    }
}
