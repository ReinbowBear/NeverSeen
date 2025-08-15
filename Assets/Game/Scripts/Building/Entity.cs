using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Action<bool> OnSelected;
    [SerializeField] protected ShapeType shapeType;
    protected Vector3Int[] shape => Shape.Shapes[shapeType];

    public byte radius;
    [HideInInspector] public List<Tile> TilesInRadius;
    [HideInInspector] public Tile Tile;


    public void Selected(bool isSelected)
    {
        foreach (var tile in TilesInRadius)
        {
            tile.ActiveTile(isSelected);
        }

        OnSelected.Invoke(isSelected);

        if (!isSelected) return;

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOScale(new Vector3(0.95f, 1.1f, 0.95f), 0.25f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
    }


    public List<Tile> GetTilesInRadius(Vector3Int center, int radius)
    {
        var tileMap = MapGenerator.Instance.TileMap;
        var result = new List<Tile>(3 * radius * (radius + 1));

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
