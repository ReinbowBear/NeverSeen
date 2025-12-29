using UnityEngine;

public class MainMenuState : MonoBehaviour, IState ,IGameState
{
    private MainMenuContext mainMenuContext = new();
    private Input input;

    public void Init(ServicesContext servicesContext)
    {
        input = servicesContext.Input;
    }


    public void Enter()
    {
        input.SwitchTo(InputMode.UI);
    }

    public void Exit()
    {

    }
}


public class MainMenuContext
{

}
