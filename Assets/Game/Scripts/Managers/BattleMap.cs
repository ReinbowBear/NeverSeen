using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour
{
    [SerializeField] private MapDataBase maps;
    [Space]
    public Dictionary<bool, Transform[]> points = new Dictionary<bool, Transform[]>(); // isPlayer = true
    [SerializeField] private Transform[] CharacterPoints; //в теории в будущем позиции будут братся из карты возможно
    [SerializeField] private Transform[] enemyPoints;
    
    private void StartedMap(MyEvent.OnEntryBattle _)
    {
        LoadMap(SaveSystem.gameData.saveChosenCharacter.chosenIndex);

        points[true] = CharacterPoints;
        points[false] = enemyPoints;
    }

    public async void LoadMap(byte index)
    {
        await Content.GetAsset(maps.containers[index].prefab, transform);
    }

    private void GetCharacter(MyEvent.OnEntityInit CharacterInstantiate)
    {
        CharacterInstantiate.entity.battleMap = this;
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntryBattle>(StartedMap);
        EventBus.Add<MyEvent.OnEntityInit>(GetCharacter);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEntryBattle>(StartedMap);
        EventBus.Remove<MyEvent.OnEntityInit>(GetCharacter);
    }  
}
