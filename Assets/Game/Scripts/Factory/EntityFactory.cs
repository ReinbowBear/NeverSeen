using System.Threading.Tasks;

public static class EntityFactory
{
    public static async Task<Entity> GetEntity(EntityContainer container)
    {
        var characterObject = await Address.GetAssetByName("CharacterPrefab");
        Entity character = characterObject.GetComponent<Entity>();

        character.Init(container.stats);

        for (byte i = 0; i < container.stats.abilitys.Length; i++)
        {
            AbilityContainer abilityContainer = Content.data.abilitys.GetItemByName(container.stats.abilitys[i]);
            character.inventory.abilitys[i] = abilityContainer;

            Ability ability = await AbilityFactory.GetAbility(abilityContainer);
            character.abilityControl.AddAbility(ability, i);
        }

        if (container.stats.isPlayer == false)
        {
            characterObject.AddComponent<EnemyLogic>();
        }

        return character;
    }
}
