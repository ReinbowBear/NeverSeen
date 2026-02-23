using System.Collections.Generic;

public class CellularAutomaton
{
    public Queue<TileData> Queue = new();
    public HashSet<TileData> Visited = new();
    public List<TileData> result = new();

    public List<TileData> Grow(ICell cellCondig, List<TileData> startTiles, int count) // метод вероятно требует доработки. например что  если часть клеток сожрана другими а колово не удовлетворено ещё
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
    IEnumerable<TileData> GetNeighbors(TileData cell);
    bool CanExpand(TileData cell);
}
