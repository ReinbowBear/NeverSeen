using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponSO weaponSO;

    private float timeToAttack;

    protected virtual IEnumerable Prepare()
    {
        while (timeToAttack < weaponSO.reloadTime)
        {
            timeToAttack += Time.deltaTime;

            yield return null;
        }
        
        Attack();
    }

    protected virtual void Attack()
    {
        Debug.Log("атака!");
        timeToAttack = 0;
    }

    protected virtual void ChangeDirection(Vector3 direction)
    {
        
    }
}
