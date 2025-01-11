using System.Collections;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [HideInInspector] public Entity character;
    [HideInInspector] public AbilitySO stats;

    [HideInInspector] public BaseTarget target;
    [HideInInspector] public BaseTrigger trigger;
    [HideInInspector] public Effect effect;

    private float cooldown;
    [HideInInspector] public Transform[] targets;

    public void Init(AbilitySO newStats)
    {
        stats = Instantiate(newStats);
        effect.data = newStats.effectStats;
    }


    public void Prepare()
    {
        if (cooldown >= stats.reloadTime)
        {
            character.combatManager.AddAction(this);
            StartCoroutine(Reload());
        }
    }

    public IEnumerator Activate()
    {
        targets = target.GetTarget(character.battleMap, character);

        for (byte i = 0; i < targets.Length; i++)
        {
            if (targets[i] == null)
            {
                continue;
            }


            Entity enemy = targets[i].GetComponentInChildren<Entity>();

            if (enemy != null && stats.damage != 0)
            {
                enemy.health.TakeDamage(stats.damage);

                if (trigger.CheckTrigger())
                {
                    effect.GetEffect(targets[i]);
                }
            }
            
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
