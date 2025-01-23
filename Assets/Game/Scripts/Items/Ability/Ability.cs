using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [HideInInspector] public Entity character;
    [HideInInspector] public AbilitySO stats;

    [HideInInspector] public BaseTarget target;
    [HideInInspector] public BaseTrigger trigger;
    [HideInInspector] public Effect effect;

    public Coroutine cooldown;
    [HideInInspector] public List<Transform> targets = new List<Transform>();

    public void Init(AbilitySO newStats)
    {
        stats = Instantiate(newStats);
        effect.stats = newStats.effectStats;
    }


    public void Prepare()
    {
        character.combatManager.AddAction(this);
    }

    public IEnumerator Activate()
    {
        targets = target.GetTarget(character.battleMap, character);

        for (byte i = 0; i < targets.Count; i++)
        {
            Entity enemy = targets[i].GetComponentInChildren<Entity>();
            if (enemy != null)
            {
                enemy.health.TakeDamage(stats.damage, stats.damageType);

                if (trigger.CheckTrigger())
                {
                    yield return effect.GetEffect(targets[i]);
                }
            }            
            yield return new WaitForSeconds(0.2f);
        }
    }

    public IEnumerator Reload()
    {
        float timeLeft = 0;
        while (timeLeft < stats.reloadTime)
        {
            timeLeft += Time.deltaTime;
            yield return null;
        }
        cooldown = null;
    }
}
