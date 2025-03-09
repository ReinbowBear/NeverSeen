using System.Collections;
using UnityEngine;

public class Melee : Weapon
{
    [Space]
    [SerializeField] protected Collider hitBox;

    protected override IEnumerator Attacking()
    {
        yield return new WaitForSeconds(prepare);
        hitBox.enabled = true;
        yield return new WaitForSeconds(attack);
        hitBox.enabled = false;
        yield return new WaitForSeconds(ending);
        corutine = null;
    }

    public override void FalseAttack()
    {
        hitBox.enabled = false;
        base.FalseAttack();
    }


    public virtual void OniggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(damage);
        }
    }
}
