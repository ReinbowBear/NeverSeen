using System.Collections;
using UnityEngine;

public class Shield : Ability
{
    [Space]
    [SerializeField] protected Collider hitBox;

    public override IEnumerator Use(Character owner)
    {
        owner.state = State.attack;
        yield return new WaitForSeconds(0);
        owner.state = State.None;
    }

    public override void Cancel(Character owner)
    {
        base.Cancel(owner);
        hitBox.enabled = false;
    }


    void OniggerEnter(Collider other)
    {
        if (other.TryGetComponent<Projectile>(out Projectile bullet))
        {
            bullet.ownerTag = transform.root.gameObject.tag;
            bullet.direction = transform.forward;
        }
    }
}
