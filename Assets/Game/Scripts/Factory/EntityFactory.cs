using UnityEngine;
using UnityEngine.AddressableAssets;

public class EntityFactory : MonoBehaviour
{
    [SerializeField] private AssetReference characterPrefab;
    [SerializeField] private AssetReference enemyPrefab;
    [Space]
    [SerializeField] private EntityDataBase gameCharacters;
    [SerializeField] private EntityDataBase gameEnemys;
    [Space]
    [SerializeField] private BattleMap battleMap;
    [SerializeField] private AbilityFactory abilityFactory;


    public async void GetCharacter(int index)
    {

        GameObject newObject = await Content.GetAsset(characterPrefab);
        Entity character = newObject.GetComponent<Entity>();

        if (character.baseStats.isPlayer)
        {
            for (byte i = 0; i < battleMap.points[true].Length; i++)
            {
                if (battleMap.points[true][i].childCount == 0)
                {
                    character.transform.SetParent(battleMap.points[true][i], false);
                }
            }
        }
        else
        {
            for (byte i = 0; i < battleMap.points[false].Length; i++)
            {
                if (battleMap.points[false][i].childCount == 0)
                {
                    character.transform.SetParent(battleMap.points[false][i], false);
                }
            }
        }

        InitCharacter(index, character);

        MyEvent.OnEntityInit newEvent = new MyEvent.OnEntityInit(character);
        EventBus.Invoke<MyEvent.OnEntityInit>(newEvent);
    }

    private async void InitCharacter(int index, Entity character)
    {
        EntitySO data = gameCharacters.containers[index].stats;

        character.model = data.model;
        
        for (byte i = 0; i < data.abilitys.Length; i++)
        {
            AbilityContainer abilityContainer = abilityFactory.GetContainerByName(data.abilitys[i]);
            character.inventory.abilitys[i] = abilityContainer;

            Ability ability = await abilityFactory.GetAbility(abilityContainer);
            character.abilityControl.AddAbility(ability, i);
        }
    }


    private void LoadCharacter(MyEvent.OnEntryBattle _)
    {
        GetCharacter(SaveSystem.gameData.saveChosenCharacter.chosenIndex);
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntryBattle>(LoadCharacter);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEntryBattle>(LoadCharacter);
    }  
}
