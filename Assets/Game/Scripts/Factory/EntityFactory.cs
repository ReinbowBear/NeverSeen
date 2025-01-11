using System.Threading.Tasks;
using UnityEngine;

public static class EntityFactory
{
    public static async Task<Entity> GetEntity(EntityContainer container)
    {
        var characterObject = await Address.GetAssetByName("CharacterPrefab");
        Entity character = characterObject.GetComponent<Entity>();
        
        for (byte i = 0; i < container.stats.abilitys.Length; i++)
        {
            AbilityContainer abilityContainer = Content.data.abilitys.GetItemByName(container.stats.abilitys[i]);
            character.inventory.abilitys[i] = abilityContainer;

            Ability ability = await AbilityFactory.GetAbility(abilityContainer);
            character.abilityControl.AddAbility(ability, i);
        }

        character.Init();
        return character;
    }
}
