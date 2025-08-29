using System.Collections.Generic;
using UnityEngine;

public static class Shape // класс с набором определённых форм для гексагональной сетки (кубические координаты)
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
