using System.Collections;
using UnityEngine;

public class Effect
{
    public EffectSO data;

    public virtual void GetEffect(Transform target)
    {
        Entity enemy = target.GetComponentInChildren<Entity>();

        if (enemy != null)
        {
            enemy.effectControl.AddEffect(this);
        }
    }

    public virtual IEnumerator DoEffect(Entity character)
    {
        yield return new WaitForSeconds(1);
    }
}
