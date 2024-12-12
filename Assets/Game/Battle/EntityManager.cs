using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPoints;
    [SerializeField] private GameObject[] enemyPoints;


    private void StartedCharacter()
    {

    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntryBattle>(StartedCharacter);
    }
}
