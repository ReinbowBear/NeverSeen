using System.Threading.Tasks;
using UnityEngine;

public static class EntityFactory
{
    public static async Task<Entity> GetEntity(EntitySO entitySO)
    {
        var characterObject = await Address.GetAssetByName("CharacterPrefab");
        Entity character = characterObject.GetComponent<Entity>();

        character.Init(entitySO);

        for (byte i = 0; i < entitySO.abilitys.Length; i++)
        {
            Ability ability = await AbilityFactory.GetAbility(entitySO.abilitys[i]);
            
            character.abilityControl.AddAbility(ability, i);
        }

        if (entitySO.isPlayer == false)
        {
            characterObject.AddComponent<EnemyLogic>();
        }

        return character;
    }
}
