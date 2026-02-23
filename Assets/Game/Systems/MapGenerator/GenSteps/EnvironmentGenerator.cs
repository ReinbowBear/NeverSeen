using UnityEngine;

public class EnvironmentGenerator
{
    public byte OresTiles;

    public MapGenContext PrepareData;
    public MyRandom Random;

    public EnvironmentGenerator(MapGenContext prepareData, MyRandom random)
    {
        PrepareData = prepareData;
        Random = random;
    }


    public void Generate(GameObject environment, EnvironmentRule environmentRule, int count)
    {

    }
}
