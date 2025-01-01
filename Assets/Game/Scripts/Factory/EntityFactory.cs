using UnityEngine;
using UnityEngine.AddressableAssets;

public class EntityFactory : MonoBehaviour
{
    [SerializeField] private AssetReference characterPrefab;
    [SerializeField] private AssetReference enemyPrefab;
    [Space]
    [SerializeField] private EntityDataBase characterDataBase;
    [SerializeField] private EntityDataBase enemyDataBase;
    [Space]
    [SerializeField] private BattleMap battleMap;


    public async void GetCharacter(int index)
    {
        for (byte i = 0; i < battleMap.CharacterPoints.Length; i++)
        {
            if (battleMap.CharacterPoints[i].childCount == 0)
            {
                GameObject newObject = await Content.GetAsset(characterPrefab, battleMap.CharacterPoints[i]);
                Character character = newObject.GetComponent<Character>();
                InitCharacter(index, character);

                MyEvent.OnCharacterInit newEvent = new MyEvent.OnCharacterInit(character);
                EventBus.Invoke<MyEvent.OnCharacterInit>(newEvent);
                break;
            }
        }
    }

    public async void GetEnemy(int index)
    {
        for (byte i = 0; i < battleMap.enemyPoints.Length; i++)
        {
            if (battleMap.enemyPoints[i].childCount == 0)
            {
                GameObject newObject = await Content.GetAsset(enemyPrefab, battleMap.enemyPoints[i]);
                Enemy enemy = newObject.GetComponent<Enemy>();
                InitEnemy(index, enemy);

                MyEvent.OnEnemyInit newEvent = new MyEvent.OnEnemyInit(enemy);
                EventBus.Invoke<MyEvent.OnEnemyInit>(newEvent);
                break;
            }
        }
    }

    private void InitCharacter(int index, Character character)
    {
        Debug.Log("типа инициализация должна бЫть");
    }

    private void InitEnemy(int index, Enemy enemy)
    {
        Debug.Log("типа инициализация должна бЫть");
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
