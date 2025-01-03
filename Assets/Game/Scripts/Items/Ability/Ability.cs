using System.Collections;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [HideInInspector] public Entity character;

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
            StartCoroutine(Reload());
        }
    }

    public IEnumerator Activate()
    {
        Entity[] enemys = target.GetTarget(character.battleMap.points[!character.baseStats.isPlayer]);

        foreach (Entity entity in enemys)
        {
            if (entity == null)
            {
                continue;
            }


            if (trigger != null && trigger.CheckTrigger() && effect != null)
            {
                effect.GetEffect(entity);
            }
            
            entity.health.TakeDamage(stats.damage);

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
