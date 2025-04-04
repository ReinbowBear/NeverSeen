using UnityEngine;
using System.Collections;

public abstract class Weapon : Item
{
    public float prepare;
    public float attack;
    public float ending;
    [Space]
    [SerializeField] protected byte damage;

    [HideInInspector] public bool isInAttack;


    public virtual IEnumerator Attack()
    {
        isInAttack = true;
        yield return new WaitForSeconds(prepare);

        yield return new WaitForSeconds(attack);

        yield return new WaitForSeconds(ending);
        isInAttack = false;
    }

    public virtual void FalseAttack()
    {
        StopAllCoroutines();
        isInAttack = false;
    }
}
