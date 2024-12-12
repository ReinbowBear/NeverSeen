using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private byte turnCount;


    private void NextTurn()
    {
        
    }

    private void CheckBattle()
    {
        if (turnCount == 0)
        {
            EventBus.Invoke<MyEvent.OnEndLevel>();
        }
    }
}
