using System.Collections;
using UnityEngine;

public abstract class Applicable : MonoBehaviour //класс переходник, что бы можно было запускать способности и оружия в CombatManager
{
    public virtual IEnumerator Reload()
    {
        yield return null;
    }

    public virtual void Activate()
    {

    }

    public virtual IEnumerator Attacking()
    {
        StartCoroutine(Reload());
        yield return null;
    }


    public virtual void False()
    {
        StopAllCoroutines();
    }
}
