using UnityEngine;

[System.Serializable]
public class SpawnCondition
{
    public Vector2 HeightRange;
    public Vector2 TemperatureRange;
    public Vector2 MoistureRange;

    public float GetScore(Tile tile)
    {
        float score = 0;

        score += RangeScore(tile.Height, HeightRange);
        score += RangeScore(tile.Temperature, TemperatureRange);
        score += RangeScore(tile.Moisture, MoistureRange);

        return score;
    }

    private float RangeScore(float value, Vector2 range)
    {
        if (value < range.x || value > range.y) return -10;

        float center = (range.x + range.y) * 0.5f;
        float distance = Mathf.Abs(value - center);

        return 1f - distance;
    }
}
