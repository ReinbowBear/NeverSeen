using System.Collections;
using UnityEngine;

public class Melee : Weapon
{
    [Space]
    [SerializeField] protected Collider hitBox;

    public override IEnumerator Attack()
    {
        isInAttack = true;
        yield return new WaitForSeconds(prepare);
        hitBox.enabled = true;
        yield return new WaitForSeconds(attack);
        hitBox.enabled = false;
        yield return new WaitForSeconds(ending);
        isInAttack = false;
    }

    public override void FalseAttack()
    {
        base.FalseAttack();
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
