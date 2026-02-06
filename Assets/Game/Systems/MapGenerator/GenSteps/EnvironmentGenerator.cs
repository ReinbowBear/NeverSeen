
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


    public void Generate(EnvironmentRule environmentRule)
    {

    }
}

[System.Serializable]
public struct EnvironmentRule
{
    public int minCount;
    public int maxCount;
}

[System.Serializable]
public struct EnvironmentData
{
    public string ID;
    public string PrefabName;
}
