using System.Collections;
using UnityEngine;

public class Launch : Weapon
{
    public override IEnumerator Attack()
    {
        isInAttack = true;
        yield return new WaitForSeconds(prepare);

        yield return new WaitForSeconds(attack);

        yield return new WaitForSeconds(ending);
        isInAttack = false;
    }
}
