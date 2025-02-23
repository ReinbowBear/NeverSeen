using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private BattleMap battleMap;
    private List<CharacterSO> toDoEntities = new List<CharacterSO>();

    public void AddEntity(CharacterSO newEntity)
    {
        if (newEntity != null)
        {
            toDoEntities.Add(newEntity);
        }
        else if (toDoEntities.Count == 0)
        {
            return;
        }

        bool side = toDoEntities[0].isPlayer;
        for (byte i = 0; i < battleMap.points[side].Length; i++)
        {
            if (battleMap.points[side][i].childCount == 0)
            {
                MakeEntity(toDoEntities[0], side, i);
                toDoEntities.RemoveAt(0);
                break;
            }
        }
    }
    
    private async void MakeEntity(CharacterSO newEntity, bool side ,byte pos)
    {
        Entity newCharacter = await EntityFactory.GetEntity(newEntity);

        MyEvent.OnEntityInit newEvent = new MyEvent.OnEntityInit(newCharacter);
        EventBus.Invoke<MyEvent.OnEntityInit>(newEvent);

        newCharacter.transform.SetParent(battleMap.points[side][pos], false);
        newCharacter.move.pos = pos;
        MoveToPos(newCharacter);
    }

    public void MoveToPos(Entity newCharacter)
    {
        newCharacter.transform.localScale = new Vector3(0.8f, 1.4f, 0.8f);
        DOTween.Sequence()
            .SetLink(newCharacter.gameObject)
            .Append(newCharacter.transform.DOMove(new Vector3(newCharacter.transform.position.x, 8, newCharacter.transform.position.z), 0.75f).From()).SetEase(Ease.Linear)
            .Append(newCharacter.transform.DOScale(new Vector3(1.2f, 0.8f, 1.2f), 0.15f)).SetEase(Ease.Linear)
            .Append(newCharacter.transform.DOScale(new Vector3(1, 1, 1), 0.25f))
            .OnComplete(() => { newCharacter.CanAttack(); });
    }


    private void LoadCharacter(MyEvent.OnEntryBattle _)
    {
        byte index = SaveSystem.gameData.generalData.characteIndex;
        //CharacterSO entity = Content.instance.characters.containers[index];
        //AddEntity(entity);
    }

    private void OnDeathAddEntity(MyEvent.OnEnemyDeath _)
    {
        AddEntity(null);
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntryBattle>(LoadCharacter);
        EventBus.Add<MyEvent.OnEnemyDeath>(OnDeathAddEntity);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEntryBattle>(LoadCharacter);
        EventBus.Remove<MyEvent.OnEnemyDeath>(OnDeathAddEntity);
    }
}
