using System;
using System.Threading.Tasks;
using UnityEngine;

public static class AbilityFactory
{    
    public static async Task<Ability> GetAbility(AbilitySO abilitySO)
    {
        var abilityObject = await Address.GetAssetByName("AbilityPrefab");
        Ability ability = abilityObject.GetComponent<Ability>();

        Type targetType = Type.GetType(abilitySO.targetType.ToString());
        Type triggerType = Type.GetType(abilitySO.triggerType.ToString());
        Type effectType = Type.GetType(abilitySO.effectType.ToString());

        BaseTarget targetClass = (BaseTarget)Activator.CreateInstance(targetType);
        BaseTrigger triggerClass = (BaseTrigger)Activator.CreateInstance(triggerType);
        Effect effectClass = (Effect)Activator.CreateInstance(effectType);

        ability.target = targetClass;
        ability.trigger = triggerClass;
        ability.effect = effectClass;

        ability.Init(abilitySO);
        
        return ability;
    }
}
