using System.Collections.Generic;

public class CellularAutomaton
{
    public Queue<Tile> Queue = new();
    public HashSet<Tile> Visited = new();
    public List<Tile> result = new();

    public List<Tile> Grow(ICell cellCondig, List<Tile> startTiles, int count)
    {
        Queue.Clear();
        Visited.Clear();
        result.Clear();

        foreach (var cell in startTiles)
        {
            Queue.Enqueue(cell);
            Visited.Add(cell);
        }

        while (Queue.Count > 0 && result.Count < count)
        {
            var current = Queue.Dequeue();
            result.Add(current);

            foreach (var neighbor in cellCondig.GetNeighbors(current))
            {
                if (Visited.Contains(neighbor)) continue;
                
                if (cellCondig.CanExpand(neighbor) == true)
                {
                    Queue.Enqueue(neighbor);
                    Visited.Add(neighbor);
                }
            }
        }

        return result;
    }
}


public interface ICell
{
    IEnumerable<Tile> GetNeighbors(Tile cell);
    bool CanExpand(Tile cell);
}


public struct DefaultCell : ICell
{
    private RandomService Random;

    public DefaultCell(RandomService random)
    {
        Random = random;
    }


    public IEnumerable<Tile> GetNeighbors(Tile tile)
    {
        return tile.Neighbors;
    }

    public bool CanExpand(Tile tile)
    {
        if (tile.BiomeType != BiomeType.Water) return false;
        if (tile.BiomeType != BiomeType.Snow) return false;

        return Random.NextFloat() < 0.7f;
    }
}
