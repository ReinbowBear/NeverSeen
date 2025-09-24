using UnityEngine;
using Zenject;

public class MyRandom : IInitializable
{
    private GeneralData generalData;
    public System.Random System { get; private set; }

    public MyRandom(GeneralData generalData)
    {
        this.generalData = generalData;
    }

    public void Initialize()
    {
        SetSeed();
    }


    public void LoadSeed()
    {
        SetSeed(generalData.Seed);
    }

    public void SetSeed(int newSeed = 0)
    {
        if (newSeed == 0)
        {
            newSeed = global::System.DateTime.Now.Millisecond;
            generalData.Seed = newSeed;
        }

        System = new System.Random(newSeed);
        Random.InitState(newSeed);
    }
}
