using UnityEngine;

public class MyRandom
{
    private GeneralData generalData;
    public System.Random System { get; private set; }

    public MyRandom(GeneralData generalData)
    {
        this.generalData = generalData;
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


    //public async Task<string> GetRandomKey(string label)
    //{
    //    var locations = await Addressables.LoadResourceLocationsAsync(label).Task;
//
    //    if (locations == null || locations.Count == 0)
    //    {
    //        Debug.LogError("Нет предметов с меткой: " + label);
    //        return null;
    //    }
//
    //    var randomIndex = Random.Range(0, locations.Count);
    //    var item = locations[randomIndex];
//
    //    return item.PrimaryKey;
    //}
}
