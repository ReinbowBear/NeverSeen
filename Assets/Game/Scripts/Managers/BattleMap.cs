using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour
{
    public Dictionary<bool, Transform[]> points = new Dictionary<bool, Transform[]>(); // isPlayer = true
    [SerializeField] private Transform[] CharacterPoints; //в теории в будущем позиции будут братся из карты возможно
    [SerializeField] private Transform[] enemyPoints;

    private GameObject map;

    void Awake()
    {
        points[true] = CharacterPoints;
        points[false] = enemyPoints;
    }


    public async void LoadMap(MapContainer container)
    {
        map = await Address.GetAsset(container.prefab, transform);
    }


    private void GetCharacter(MyEvent.OnEntityInit CharacterInstantiate)
    {
        CharacterInstantiate.entity.battleMap = this;
    }

    private void StartedMap(MyEvent.OnEntryBattle _)
    {
        byte index = SaveSystem.gameData.saveChosenCharacter.chosenIndex;
        LoadMap(Content.data.maps.containers[index]);
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
