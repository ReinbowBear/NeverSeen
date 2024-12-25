using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private CharacterDataBase gameCharacters;
    [SerializeField] private EnemyDataBase gameEnemys;
    [Space]
    [SerializeField] private BattleMap battleMap;

    [HideInInspector] public Character[] characters;
    [HideInInspector] public Enemy[] enemys;

    void Awake()
    {
        characters = new Character[battleMap.CharacterPoints.Length];
        enemys = new Enemy[battleMap.CharacterPoints.Length];
    }


    private void LoadCharacter(MyEvent.OnEntryBattle _)
    {
        AddCharacter(SaveSystem.gameData.saveChosenCharacter.chosenIndex);
    }

    
    public async void AddCharacter(byte index)
    {
        for (byte i = 0; i < characters.Length; i++)
        {
            if (characters[i] = null)
            {
                GameObject newObject = await Content.GetAsset(gameCharacters.containers[index].prefab, battleMap.CharacterPoints[i]);
                characters[i] = newObject.GetComponent<Character>();
                
                characters[i].entityManager = this;
                MyEvent.OnCharacterInit newCharacterEvent = new MyEvent.OnCharacterInit(characters[i]);
                EventBus.Invoke<MyEvent.OnCharacterInit>(newCharacterEvent);
                
                break;
            }
        }
    }

    public async void AddEnemy(int index)
    {
        for (byte i = 0; i < enemys.Length; i++)
        {
            if (enemys[i] = null)
            {
                GameObject newObject = await Content.GetAsset(gameEnemys.containers[index].prefab, battleMap.enemyPoints[i]);
                enemys[i] = newObject.GetComponent<Enemy>();

                enemys[i].entityManager = this;
                break;
            }
        }
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
