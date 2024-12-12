using System.Collections;
using UnityEngine;

public class Ability : MonoBehaviour
{    
    [SerializeField] protected AbilitySO weaponSO;

    private float timeToReload;

    protected virtual IEnumerable Reload()
    {
        //while (timeToAttack < weaponSO.speed)
        {
            timeToReload += Time.deltaTime;

            yield return null;
        }
        
        Attack();
    }

    protected virtual void Attack()
    {
        Debug.Log("атака!");
        timeToReload = 0;
    }
}
