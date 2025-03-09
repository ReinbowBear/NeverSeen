using UnityEngine;
using System.Collections;

public abstract class Weapon : ItemData
{
    [SerializeField] protected float prepare;
    [SerializeField] protected float attack;
    [SerializeField] protected float ending;
    [Space]
    [SerializeField] protected byte damage;

    [HideInInspector] public Coroutine corutine;


    public virtual void Attack()
    {
        corutine = StartCoroutine(Attacking());
    }


    protected virtual IEnumerator Attacking()
    {
        yield return new WaitForSeconds(prepare);

        yield return new WaitForSeconds(attack);

        yield return new WaitForSeconds(ending);
        corutine = null;
    }

    public virtual void FalseAttack()
    {
        corutine = null;
        StopAllCoroutines();
    }
}
