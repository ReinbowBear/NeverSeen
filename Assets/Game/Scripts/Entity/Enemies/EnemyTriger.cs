using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriger
{
    public virtual bool CheckTriger(Entity character)
    {
        return true;
    }

    public virtual void DoAction(Entity character)
    {
        
    }
}
