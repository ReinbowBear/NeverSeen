using UnityEngine;

public class MyRandom
{
    private GameState gameState;
    public System.Random System { get; private set; }

    public MyRandom(GameState gameState)
    {
        this.gameState = gameState;
    }


    public void LoadSeed()
    {
        SetSeed(gameState.seed);
    }

    public void SetSeed(int newSeed = 0)
    {
        if (newSeed == 0)
        {
            newSeed = global::System.DateTime.Now.Millisecond;
            gameState.seed = newSeed;
        }

        System = new System.Random(newSeed);
        Random.InitState(newSeed);
    }
}
