using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Action<bool> OnSelected;

    [SerializeField] protected ShapeType shapeType;
    [SerializeField] protected byte radius;
    protected List<Tile> tilesInRadius;
    protected Tile tile;

    protected Vector3Int[] shape => Shape.Shapes[shapeType];
    public byte Radius => radius;
    public List<Tile> TilesInRadius => tilesInRadius;
    public Tile Tile => tile;


    public void Selected(bool isSelected)
    {
        foreach (var tile in tilesInRadius)
        {
            tile.SetBacklight(isSelected);
        }

        OnSelected?.Invoke(isSelected);

        if (!isSelected) return;

        TweenAnimation.Impact(transform);
    }
}
