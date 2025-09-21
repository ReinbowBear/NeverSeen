using UnityEngine;

public class EntityView : MonoBehaviour
{
    [SerializeField] private Entity entity;

    private void OnSelected(bool isSelected)
    {
        foreach (var tile in entity.TilesInRadius)
        {
            tile.SetBacklight(isSelected);
        }

        if (!isSelected) return;

        Tween.Impact(transform);
    }


    void OnEnable()
    {
        entity.OnSelected += OnSelected;
    }

    void OnDisable()
    {
        entity.OnSelected -= OnSelected;
    }
}
