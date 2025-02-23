using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public Entity character;
    [HideInInspector] public AbilitySO stats;

    [HideInInspector] public Effect effect;

    public Coroutine cooldown;
    [HideInInspector] public List<Transform> targets = new List<Transform>();

    public void Init(AbilitySO newStats)
    {
        stats = Instantiate(newStats);
        effect.stats = newStats.effect;
    }


    public IEnumerator Activate()
    {
        //targets = target.GetTarget(character);

        for (byte i = 0; i < targets.Count; i++)
        {
            Entity enemy = targets[i].GetComponentInChildren<Entity>();
            
            if (enemy != null)
            {
                enemy.health.TakeDamage(stats.damage, stats.damageType);
                yield return effect.GetEffect(targets[i]);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    public IEnumerator Reload(BarChange abilityBar)
    {
        float maxTime = stats.reloadTime / character.currentStats.reloadMultiplier;
        float timeLeft = maxTime;
        while (timeLeft > 0)
        {
            abilityBar.ChangeBar(maxTime, timeLeft);
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        cooldown = null;
    }
}
