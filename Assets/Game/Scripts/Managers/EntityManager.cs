using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private CharacterDataBase gameCharacters;
    [SerializeField] private EnemyDataBase gameEnemys;
    [Space]
    [SerializeField] private BattleMap battleMap;


    public async void AddCharacter(byte index)
    {
        for (byte i = 0; i < battleMap.CharacterPoints.Length; i++)
        {
            if (battleMap.CharacterPoints[i].childCount == 0)
            {
                GameObject newObject = await Content.GetAsset(gameCharacters.containers[index].prefab, battleMap.CharacterPoints[i]);

                MyEvent.OnCharacterInit newCharacterEvent = new MyEvent.OnCharacterInit(newObject.GetComponent<Character>());
                EventBus.Invoke<MyEvent.OnCharacterInit>(newCharacterEvent);
                
                break;
            }
        }
    }

    public async void AddEnemy(int index)
    {
        for (byte i = 0; i < battleMap.enemyPoints.Length; i++)
        {
            if (battleMap.enemyPoints[i].childCount == 0)
            {
                GameObject newObject = await Content.GetAsset(gameEnemys.containers[index].prefab, battleMap.enemyPoints[i]);

                break;
            }
        }
    }


    private void LoadCharacter(MyEvent.OnEntryBattle _)
    {
        AddCharacter(SaveSystem.gameData.saveChosenCharacter.chosenIndex);
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
