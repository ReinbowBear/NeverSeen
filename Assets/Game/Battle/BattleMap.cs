using UnityEngine;

public class BattleMap : MonoBehaviour
{
    private void LoadMap()
    {
        Debug.Log("бзззз.... загрузка карты типа");
    }

    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntryBattle>(LoadMap);
    }  
}
