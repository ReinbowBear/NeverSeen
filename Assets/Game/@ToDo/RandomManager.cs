using UnityEngine;

public static class RandomManager
{
    public static System.Random random;

    static RandomManager()
    {
        SetSeed(GameData.seed);
    }


    public static void SetSeed(int newSeed = 0)
    {
        if (newSeed == 0)
        {
            newSeed = System.DateTime.Now.Millisecond;
            GameData.seed = newSeed;
        }

        random = new System.Random(newSeed);
        Random.InitState(newSeed);
    }
    
    // int test = System.Random.Next(2, 10);
    // float pitch = Random.Range(minPitch, maxPitch);
}
