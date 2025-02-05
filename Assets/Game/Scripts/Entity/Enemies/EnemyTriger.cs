using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriger
{
    public virtual bool CheckTriger(Entity character)
    {
        if (character.inventory.abilities[0].target.GetTarget(character)[0] != null)
        {
            return true;
        }

        return false;
    }

    public virtual void DoAction(Entity character)
    {
        
    }
}
