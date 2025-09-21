using UnityEngine;
using Zenject;

public class MyRandom : IInitializable
{
    private GameData gameState;
    public System.Random System { get; private set; }

    public MyRandom(GameData gameState)
    {
        this.gameState = gameState;
    }

    public void Initialize()
    {
        SetSeed();
    }


    public void LoadSeed()
    {
        SetSeed(gameState.General.Seed);
    }

    public void SetSeed(int newSeed = 0)
    {
        if (newSeed == 0)
        {
            newSeed = global::System.DateTime.Now.Millisecond;
            gameState.General.Seed = newSeed;
        }

        System = new System.Random(newSeed);
        Random.InitState(newSeed);
    }
}
