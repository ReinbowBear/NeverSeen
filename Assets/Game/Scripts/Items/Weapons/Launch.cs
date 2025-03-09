using System.Collections;
using UnityEngine;

public class Launch : Weapon
{
    protected override IEnumerator Attacking()
    {
        yield return new WaitForSeconds(prepare);

        yield return new WaitForSeconds(attack);

        yield return new WaitForSeconds(ending);
        corutine = null;
    }
}
