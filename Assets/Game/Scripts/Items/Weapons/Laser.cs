using System.Collections;
using UnityEngine;

public class Laser : Ability
{
    [Space]
    [SerializeField] protected short damage;
    [SerializeField] protected byte maxBounces;

    public override IEnumerator Use(Character owner)
    {
        owner.state = State.attack;
        yield return new WaitForSeconds(prepare);

        Vector3 currentOrigin = transform.position;
        Vector3 currentDirection = transform.forward;

        for (byte i = 0; i < maxBounces; i++)
        {
            RaycastHit[] hits = Physics.RaycastAll(currentOrigin, currentDirection, 50, LayerMask.GetMask("Default"));
            foreach (var hit in hits)
            {
                if (hit.transform.TryGetComponent<Health>(out Health health))
                {
                    health.TakeDamage(damage);
                }
                else
                {
                    currentOrigin = hit.point;
                    currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                    break;
                }
            }
        }

        yield return new WaitForSeconds(attack);

        yield return new WaitForSeconds(ending);
        owner.state = State.None;
    }
}
