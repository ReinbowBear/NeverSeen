using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class EffectFactory : MonoBehaviour
{
    //[SerializeField] private EffectDataBase gameEffects;
    [SerializeField] private AssetReference effectPref;

    public async Task<Ability> GetAbility(AbilityContainer ability)
    {
        Type targetType = Type.GetType(ability.stats.targetType.ToString());
        Type triggerType = Type.GetType(ability.stats.triggerType.ToString());
        Type effectType = Type.GetType(ability.stats.effectType.ToString());

        BaseTarget targetClass = (BaseTarget)Activator.CreateInstance(targetType);
        BaseTrigger triggerClass = (BaseTrigger)Activator.CreateInstance(triggerType);
        BaseEffect effectClass = (BaseEffect)Activator.CreateInstance(effectType);


        var AbilityObject = await Address.GetAsset(effectPref);
        Ability newAbility = AbilityObject.GetComponent<Ability>();

        newAbility.target = targetClass;
        newAbility.trigger = triggerClass;
        newAbility.effect = effectClass;
        
        return newAbility;
    }
}
