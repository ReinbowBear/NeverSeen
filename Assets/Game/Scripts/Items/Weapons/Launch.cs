using System.Collections;
using UnityEngine;

public class Launch : Ability
{
    [SerializeField] protected byte damage;

    public override IEnumerator Use(Character owner)
    {
        owner.state = State.attack;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 20, LayerMask.GetMask("Ground")))
        {
            
        }
        yield return new WaitForSeconds(prepare);

        yield return new WaitForSeconds(attack);

        yield return new WaitForSeconds(ending);
        owner.state = State.None;
    }
}
