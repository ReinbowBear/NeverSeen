using System;
using System.Threading.Tasks;
using UnityEngine;

public static class AbilityFactory
{    
    public static async Task<Ability> GetAbility(AbilitySO abilitySO)
    {
        var abilityObject = await Address.GetAssetByName("AbilityPrefab");
        Ability ability = abilityObject.GetComponent<Ability>();

        Type targetType = Type.GetType(abilitySO.targetStats.targetType.ToString());
        Type effectType = Type.GetType(abilitySO.effectStats.effectType.ToString());

        BaseTarget targetClass = (BaseTarget)Activator.CreateInstance(targetType);
        Effect effectClass = (Effect)Activator.CreateInstance(effectType);

        ability.target = targetClass;
        ability.effect = effectClass;

        ability.Init(abilitySO);
        
        return ability;
    }
}
