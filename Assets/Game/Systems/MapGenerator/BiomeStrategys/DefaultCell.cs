using System.Collections.Generic;

public struct DefaultCell : ICell
{
    private MyRandom Random;

    public DefaultCell(MyRandom random)
    {
        Random = random;
    }


    public IEnumerable<TileData> GetNeighbors(TileData tile)
    {
        return tile.Neighbors;
    }

    public bool CanExpand(TileData tile)
    {
        if (tile.BiomeType != BiomeType.Ground) return false;
        if (tile.BiomeType != BiomeType.Hill) return false;

        return Random.System.NextDouble() < 0.7f;
    }
}
