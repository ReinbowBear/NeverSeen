using UnityEngine;

public class GamePlayState : MonoBehaviour, IState ,IGameState
{
    private GamePlayContext gamePlayContext = new();
    private Input input;

    private ElectoNetwork electoNetwork;
    private MyRandom myRandom;
    private TimeManager timeManager;

    public void Init(ServicesContext servicesContext)
    {
        input = servicesContext.Input;

        electoNetwork = new();
        timeManager = new();

        myRandom = new(gamePlayContext.GeneralData);
    }


    public void Enter()
    {
        input.SwitchTo(InputMode.GamePlay);
    }

    public void Exit()
    {

    }
}


public class GamePlayContext
{
    public GeneralData GeneralData = new();
    public TileMap TileMap = new();
    public InventoryData InventoryData = new();
}
