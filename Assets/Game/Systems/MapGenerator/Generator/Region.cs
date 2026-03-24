using System.Collections.Generic;
using UnityEngine;

public class Region
{
    public Vector2 Center;
    public List<Tile> Hexes = new();

    //public RegionType Type; // MountainRegion, DesertRegion и т.д.

    public List<BiomeSO> AllowedBiomes;
}

public class RegionSettings
{
    public float HeightModifier;
    public float MoistureModifier;
    public float VillageDensity;
}
