using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private BattleMap battleMap;
    
    public async void AddEntity(EntityContainer container)
    {
        Entity newEntity = await EntityFactory.GetEntity(container);
        bool side = newEntity.baseStats.isPlayer;

        for (byte i = 0; i < battleMap.points[side].Length; i++)
        {
            if (battleMap.points[side][i].childCount == 0)
            {
                newEntity.transform.SetParent(battleMap.points[side][i], false);
                break;
            }
        }

        MyEvent.OnEntityInit newEvent = new MyEvent.OnEntityInit(newEntity);
        EventBus.Invoke<MyEvent.OnEntityInit>(newEvent);
    }


    public void LoadCharacter(MyEvent.OnEntryBattle _)
    {
        byte index = SaveSystem.gameData.saveChosenCharacter.chosenIndex;
        EntityContainer container = Content.data.characters.containers[index];
        AddEntity(container);
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
