using System.Threading.Tasks;
using UnityEngine;

public class Effect
{
    public EffectSO stats;

    public virtual async Task GetEffect(Transform target)
    {
        Entity enemy = target.GetComponentInChildren<Entity>();

        if (enemy != null)
        {
            await enemy.effectControl.AddEffect(this);
        }
    }

    public virtual void DoEffect(Entity character)
    {

    }

    public virtual void FalseEffect(Entity character)
    {

    }
}
