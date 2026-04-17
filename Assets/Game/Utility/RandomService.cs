using System;
using System.Collections.Generic;

public class RandomService
{
    private System.Random random;
    public int Seed { get; private set; }

    public int CreateSeed()
    {
        Seed = Environment.TickCount;
        random = new System.Random(Seed);
        return Seed;
    }

    public void SetSeed(int seed)
    {
        Seed = seed;
        random = new System.Random(seed);
    }


    public int NextInt(int min, int max)
    {
        return random.Next(min, max);
    }

    public float NextFloat() // возращает от 0 до 0.999999
    {
        return (float)random.NextDouble();
    }


    public T GetRandomElement<T>(List<T> values)
    {
        int index = random.Next(values.Count);
        return values[index];
    }

    public List<T> GetRandomElements<T>(List<T> values, int count)
    {
        if (count >= values.Count) return new List<T>(values);

        var copy = new List<T>(values);
        var result = new List<T>(count);

        for (int i = 0; i < count; i++)
        {
            int index = random.Next(copy.Count);
            result.Add(copy[index]);
            copy.RemoveAt(index);
        }

        return result;
    }

    public List<T> GetRandomElementsWithRepetition<T>(List<T> values, int count)
    {
        var result = new List<T>(count);
        for (int i = 0; i < count; i++)
        {
            int index = random.Next(values.Count);
            result.Add(values[index]);
        }
        return result;
    }
}
