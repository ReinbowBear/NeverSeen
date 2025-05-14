using System.Collections;
using UnityEngine;

public class Melee : Ability
{
    [SerializeField] protected byte damage;

    [Space]
    [SerializeField] protected Collider hitBox;

    public override IEnumerator Use(Character owner)
    {
        owner.state = State.attack;
        yield return new WaitForSeconds(prepare);
        hitBox.enabled = true;
        yield return new WaitForSeconds(attack);
        hitBox.enabled = false;
        yield return new WaitForSeconds(ending);
        owner.state = State.None;
    }

    public override void Cancel(Character owner)
    {
        base.Cancel(owner);
        hitBox.enabled = false;
    }


    public virtual void OniggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(damage);
        }
    }
}
