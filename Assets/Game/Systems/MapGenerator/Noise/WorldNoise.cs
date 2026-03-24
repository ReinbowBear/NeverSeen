
public class WorldNoise
{
    public INoise ContinentNoise;
    public INoise DetailNoise;
    public INoise WarpNoise;

    public INoise MoistureNoise;
    public INoise TemperatureNoise;

    public INoise FinalHeightNoise;

    public WorldNoise(WorldSettings settings)
    {
        ContinentNoise = new Noise(settings.Seed, settings.ContinentScale, settings.ContinentDetailLevel, settings.ContinentDetailStrength);
        DetailNoise = new Noise(settings.Seed + 1, settings.DetailScale, settings.DetailLevel, settings.DetailStrength);
        WarpNoise = new Noise(settings.Seed + 100, settings.WarpScale);

        MoistureNoise = new Noise(settings.Seed + 200, settings.MoistureScale, settings.MoistureDetailLevel);
        TemperatureNoise = new Noise(settings.Seed + 300, settings.TemperatureScale, settings.TemperatureDetailLevel);

        var blended = new BlendNoise(ContinentNoise, DetailNoise, ContinentNoise);
        var warped = new WarpedNoise(blended, WarpNoise, settings.WarpStrength);
        var masked = new MaskNoise(warped, settings.RadialMaskRadius, settings.RadialMaskFalloff);

        FinalHeightNoise = new ClampNoise(masked, 0f, 1f);
    }


    public void SetData(TileMap tileMap)
    {
        foreach (var tile in tileMap.Tiles.Values)
        {
            var worldPos = tileMap.CubeToWorld(tile.CubeCoord, 1);
        
            tile.Height = FinalHeightNoise.GetHeight(worldPos.x, worldPos.z);
            tile.Moisture = MoistureNoise.GetHeight(worldPos.x, worldPos.z);
            tile.Temperature = TemperatureNoise.GetHeight(worldPos.x, worldPos.z);
        }
    }
}
