using UnityEngine;

public static class MyRandom
{
    public static int seed;
    public static System.Random random;

    public static void SetSeed(int newSeed = 0)
    {
        if (newSeed == 0)
        {
            newSeed = System.DateTime.Now.Millisecond;
        }

        seed = newSeed;
        random = new System.Random(newSeed);

        SaveSystem.gameData.generalData.seed = newSeed;
    }
}
