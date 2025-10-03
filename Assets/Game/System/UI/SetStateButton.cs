using UnityEngine;

public class SetStateButton : MonoBehaviour
{
    public IStateMachine stateMachine;
    public string stateKey;

    public void SetState()
    {
        stateMachine.SetState(stateKey);
    }
}