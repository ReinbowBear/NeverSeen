using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected ShapeType shapeType;
    protected Vector3Int[] shape;
    protected Tile tile;

    [SerializeField] protected byte radius;
    [HideInInspector] public List<Tile> tilesInRadius;

    void Awake()
    {
        shape = Shape.Shapes[shapeType];
    }


    public virtual void OnSelected()
    {
        foreach (var tile in tilesInRadius)
        {
            tile.ActiveTile();
        }
    }

    public virtual void Unselected()
    {
        foreach (var tile in tilesInRadius)
        {
            tile.DeactivateTile();
        }
    }


    public List<Tile> GetTilesInRadius(Vector3Int center, int radius)
    {
        List<Tile> result = new();

        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dy = Mathf.Max(-radius, -dx - radius); dy <= Mathf.Min(radius, -dx + radius); dy++)
            {
                int dz = -dx - dy;

                Vector3Int offset = new Vector3Int(dx, dy, dz);
                Vector3Int neighborCoord = center + offset;

                if (MapGenerator.Instance.TileMap.TryGetValue(neighborCoord, out Tile tile) && tile.tileData.tileType == this.tile.tileData.tileType)
                {
                    result.Add(tile);
                }
            }
        }
        return result;
    }
}



public static class Shape
{
    public static readonly Vector3Int[] shapeBase = new Vector3Int[]
    {
        new Vector3Int( 0, 0,  0),
    };

    public static readonly Vector3Int[] shapeHex = new Vector3Int[]
    {
        new Vector3Int( 0, 0,  0),

        new Vector3Int( 1, -1,  0),
        new Vector3Int( 1,  0, -1),

        new Vector3Int( 0,  1, -1),
        new Vector3Int(-1,  1,  0),

        new Vector3Int(-1,  0,  1),
        new Vector3Int( 0, -1,  1)
    };

    public static readonly Vector3Int[] shapeBigHex = new Vector3Int[]
    {
        new Vector3Int( 0, 0,  0),

        new Vector3Int( 2, -1, -1),
        new Vector3Int( 2, -2,  0),
        new Vector3Int( 1, -2,  1),

        new Vector3Int( 0, -2,  2),
        new Vector3Int(-1, -1,  2),
        new Vector3Int(-2,  0,  2),

        new Vector3Int(-2,  1,  1),
        new Vector3Int(-2,  2,  0),
        new Vector3Int(-1,  2, -1),

        new Vector3Int( 0,  2, -2),
        new Vector3Int( 1,  1, -2),
        new Vector3Int( 2,  0, -2)
    };

    public static readonly Vector3Int[] shapeParallelogram = new Vector3Int[]
    {
        new Vector3Int(0, 0, 0),

        new Vector3Int(1, -1, 0),
        new Vector3Int(0, -1, 1),
        new Vector3Int(1, -2, 1)
    };

    public static readonly Vector3Int[] shapeTriangle = new Vector3Int[]
    {
        new Vector3Int( 0, 0,  0),

        new Vector3Int(1, -1, 0),
        new Vector3Int(2, -2, 0)
    };

    public static readonly Vector3Int[] shapeSpider = new Vector3Int[]
    {
        new Vector3Int( 0, 0, 0),

        new Vector3Int( 1, -1, 0),
        new Vector3Int(-1, 0, 1),
        new Vector3Int( 0, 1, -1)
    };

    public static readonly Vector3Int[] shapeLine = new Vector3Int[]
    {
        new Vector3Int(0, 0, 0),

        new Vector3Int(1, -1, 0),
        new Vector3Int(2, -2, 0)
    };

    public static readonly Vector3Int[] shapeHalfHex = new Vector3Int[]
    {
        new Vector3Int( 0, 0,  0),

        new Vector3Int(1, -1, 0),
        new Vector3Int(1, 0, -1),
        new Vector3Int(0, 1, -1)
    };


    public static readonly Dictionary<ShapeType, Vector3Int[]> Shapes = new() // словарь обязан быть после инициализации всех массивов
    {
        { ShapeType.Base, shapeBase },
        { ShapeType.Hex, shapeHex },
        { ShapeType.BigHex, shapeBigHex },
        { ShapeType.Parallelogram, shapeParallelogram },
        { ShapeType.Triangle, shapeTriangle },
        { ShapeType.Spider, shapeSpider },
        { ShapeType.Line, shapeLine },
        { ShapeType.HalfHex, shapeHalfHex }
    };
}

public enum ShapeType
{
    Base,
    Hex,
    BigHex,
    Parallelogram,
    Triangle,
    Spider,
    Line,
    HalfHex
}
