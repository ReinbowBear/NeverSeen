using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AbilityFactory : MonoBehaviour
{
    [SerializeField] private AbilityDataBase gameAbilitys;
    [SerializeField] private AssetReference abilityPrefab;

    public async Task<Ability> GetAbility(AbilitySO ability)
    {
        Type targetType = Type.GetType(ability.targetType.ToString());
        Type triggerType = Type.GetType(ability.triggerType.ToString());
        Type effectType = Type.GetType(ability.effectType.ToString());

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


    private void GetCharacter(MyEvent.OnCharacterInit CharacterInstantiate)
    {
        CharacterInstantiate.character.abilityFactory = this;
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnCharacterInit>(GetCharacter);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnCharacterInit>(GetCharacter);
    }
}
