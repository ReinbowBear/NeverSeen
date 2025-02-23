using System;
using System.Threading.Tasks;
using UnityEngine;

public static class AbilityFactory
{    
    public static async Task<Weapon> GetAbility(AbilitySO abilitySO)
    {
        var abilityObject = await Address.GetAssetByName("AbilityPrefab");
        Weapon ability = abilityObject.GetComponent<Weapon>();

        //Type targetType = Type.GetType(abilitySO.target.targetType.ToString());
        Type effectType = Type.GetType(abilitySO.effect.effectType.ToString());

        //BaseTarget targetClass = (BaseTarget)Activator.CreateInstance(targetType);
        Effect effectClass = (Effect)Activator.CreateInstance(effectType);

        //ability.target = targetClass;
        ability.effect = effectClass;

        ability.Init(abilitySO);
        
        return ability;
    }
}
