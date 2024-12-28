using UnityEngine;

public class BattleMap : MonoBehaviour
{
    [SerializeField] private MapDataBase maps;
    [Space]
    public Transform[] CharacterPoints;
    public Transform[] enemyPoints;
    
    private void StartedMap(MyEvent.OnEntryBattle _)
    {
        LoadMap(SaveSystem.gameData.saveChosenCharacter.chosenIndex);
    }

    public async void LoadMap(byte index)
    {
        await Content.GetAsset(maps.containers[index].prefab, transform);
    }

    private void GetCharacter(MyEvent.OnCharacterInit CharacterInstantiate)
    {
        CharacterInstantiate.character.battleMap = this;
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntryBattle>(StartedMap);
        EventBus.Add<MyEvent.OnCharacterInit>(GetCharacter);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEntryBattle>(StartedMap);
        EventBus.Remove<MyEvent.OnCharacterInit>(GetCharacter);
    }  
}
