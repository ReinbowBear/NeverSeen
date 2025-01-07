using System.Collections;
using UnityEngine;

public class BaseEffect
{
    [SerializeField] protected float value;
    [SerializeField] protected float duration;

    public virtual void GetEffect(Transform target)
    {
        Entity enemy = target.GetComponentInChildren<Entity>();

        enemy.modifierControl.AddEffect(this);
    }

    public virtual IEnumerator EffectCoroutine(Entity character)
    {
        yield return new WaitForSeconds(duration);
    }
}
