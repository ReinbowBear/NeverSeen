using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public UnityEvent<OnSelectedData> OnSelected;

    public EntityStats Stats;

    public void Selected(bool isSelected)
    {
        OnSelected.Invoke(new OnSelectedData
        {
            Entity = this,
            IsSelected = isSelected
        });
    }


    void OnDisable()
    {
        Selected(false);
    }
}

[System.Serializable]
public struct EntityStats
{
    public string Name;
    public SoundSO audio;
    [Space]
    public int Cost;
    public int Radius;
    public ShapeType Shape;
}

public struct OnSelectedData
{
    public Entity Entity;
    public bool IsSelected;
}