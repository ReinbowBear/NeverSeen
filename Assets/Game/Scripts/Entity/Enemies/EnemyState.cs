using System.Collections;
using UnityEngine;

public class EnemyState
{    
    public virtual IEnumerator DoAction(EnemyAI enemyAI)
    {
        yield return new WaitForSeconds(0);
    }

    public void SetState(EnemyAI enemyAI)
    {
        enemyAI.SetState("тест");
    }
}   
