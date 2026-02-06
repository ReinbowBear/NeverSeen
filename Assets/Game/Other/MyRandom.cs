using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MyRandom
{
    public int Seed;
    public System.Random System { get; private set; }

    public void SetSeed(int newSeed = 0)
    {
        if (newSeed == 0)
        {
            newSeed = global::System.DateTime.Now.Millisecond;
            Seed = newSeed;
        }

        System = new System.Random(newSeed);
        Random.InitState(newSeed);
    }


    public List<T> GetRandomElements<T>(List<T> values, int count = 1)
    {
        if (count >= values.Count) return values;

        var copy = new List<T>(values);
        var result = new List<T>(count);

        for (int i = 0; i < count; i++)
        {
            int index = System.Next(copy.Count);
            result.Add(copy[index]);
            copy.RemoveAt(index);
        }

        return result;
    }


    public async Task<string> GetRandomKey(string label)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;

        if (locations == null || locations.Count == 0)
        {
            Debug.LogError("Нет предметов с меткой: " + label);
            return null;
        }

        var randomIndex = Random.Range(0, locations.Count);
        var item = locations[randomIndex];

        return item.PrimaryKey;
    }
}
