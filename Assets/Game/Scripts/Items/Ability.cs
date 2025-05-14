using System.Collections;
using UnityEngine;

public abstract class Ability : Item
{
    public float prepare;
    public float attack;
    public float ending;

    public virtual IEnumerator Use(Character owner)
    {
        owner.state = State.attack;
        yield return new WaitForSeconds(0);
        owner.state = State.None;
    }

    public virtual void Cancel(Character owner)
    {
        StopAllCoroutines();
        owner.state = State.None;

        owner.actionShow.SetMat(0);
    }


    public IEnumerator ShowAttack(Character owner)
    {
        owner.actionShow.SetMat(1);
        yield return new WaitForSeconds(0);

        owner.actionShow.SetMat(2);
        yield return new WaitForSeconds(0);

        owner.actionShow.SetMat(3);
        yield return new WaitForSeconds(0);

        owner.actionShow.SetMat(0);
    }
}
