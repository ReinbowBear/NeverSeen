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
            AbilitySO abilitySO = Content.data.abilityDataBase.GetItemByName(entitySO.abilitys[i]);
            character.inventory.abilitys[i] = abilitySO;

            Ability ability = await AbilityFactory.GetAbility(abilitySO);
            character.abilityControl.AddAbility(ability, i);
        }

        if (entitySO.isPlayer == false)
        {
            characterObject.AddComponent<EnemyLogic>();
        }

        return character;
    }
}
