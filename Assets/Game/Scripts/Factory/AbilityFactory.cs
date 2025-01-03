using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AbilityFactory : MonoBehaviour
{
    [SerializeField] private AbilityDataBase gameAbilitys;
    [SerializeField] private AssetReference abilityPrefab;

    public AbilityContainer GetContainerByName(string abilityName)
    {
        return gameAbilitys.GetItemByName(abilityName);

    }

    public async Task<Ability> GetAbility(AbilityContainer ability)
    {
        Type targetType = Type.GetType(ability.stats.targetType.ToString());
        Type triggerType = Type.GetType(ability.stats.triggerType.ToString());
        Type effectType = Type.GetType(ability.stats.effectType.ToString());

        Target targetClass = (Target)Activator.CreateInstance(targetType);
        Trigger triggerClass = (Trigger)Activator.CreateInstance(triggerType);
        Effect effectClass = (Effect)Activator.CreateInstance(effectType);


        var AbilityObject = await Content.GetAsset(abilityPrefab);
        Ability newAbility = AbilityObject.GetComponent<Ability>();

        newAbility.target = targetClass;
        newAbility.trigger = triggerClass;
        newAbility.effect = effectClass;
        
        return newAbility;
    }
}
