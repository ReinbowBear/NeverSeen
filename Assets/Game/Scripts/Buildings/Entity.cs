using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected ShapeType shapeType;
    protected Vector3Int[] shape;

    [SerializeField] protected byte radius;
    [HideInInspector] public List<Tile> TilesInRadius;
    [HideInInspector] public Tile Tile;

    void Awake()
    {
        shape = Shape.Shapes[shapeType];
    }


    public virtual void Selected()
    {
        foreach (var tile in TilesInRadius)
        {
            tile.ActiveTile();
        }

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOScale(new Vector3(0.95f, 1.1f, 0.95f), 0.25f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
    }

    public virtual void Unselected()
    {
        foreach (var tile in TilesInRadius)
        {
            tile.DeactivateTile();
        }
    }


    public List<Tile> GetTilesInRadius(Vector3Int center, int radius)
    {
        var result = new List<Tile>(3 * radius * (radius + 1));
        var tileMap = MapGenerator.Instance.TileMap;

        for (int cubeX = -radius; cubeX <= radius; cubeX++)
        {
            for (int cubeY = Mathf.Max(-radius, -cubeX - radius); cubeY <= Mathf.Min(radius, -cubeX + radius); cubeY++)
            {
                if (cubeX == 0 && cubeY == 0) continue; // Пропустить центр

                int cubeZ = -cubeX - cubeY;
                Vector3Int offset = new Vector3Int(cubeX, cubeY, cubeZ);
                Vector3Int neighborCoord = center + offset;

                if (tileMap.TryGetValue(neighborCoord, out Tile newTile))
                {
                    result.Add(newTile);
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
    Base, Hex, BigHex, Parallelogram, Triangle, Spider, Line, HalfHex
}
