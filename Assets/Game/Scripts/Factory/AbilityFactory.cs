using System;
using System.Threading.Tasks;
using UnityEngine;

public static class AbilityFactory
{    
    public static async Task<Ability> GetAbility(AbilityContainer container)
    {
        var abilityObject = await Address.GetAssetByName("AbilityPrefab");
        Ability ability = abilityObject.GetComponent<Ability>();

        Type targetType = Type.GetType(container.stats.targetType.ToString());
        Type triggerType = Type.GetType(container.stats.triggerType.ToString());
        Type effectType = Type.GetType(container.stats.effectType.ToString());

        BaseTarget targetClass = (BaseTarget)Activator.CreateInstance(targetType);
        BaseTrigger triggerClass = (BaseTrigger)Activator.CreateInstance(triggerType);
        Effect effectClass = (Effect)Activator.CreateInstance(effectType);

        ability.target = targetClass;
        ability.trigger = triggerClass;
        ability.effect = effectClass;

        ability.Init(container.stats);
        
        return ability;
    }
}
