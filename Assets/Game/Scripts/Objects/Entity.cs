using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public UnityEvent OnSelected;
    public UnityEvent OnDiselected;

    public EntityStats Stats;

    public void Selected(bool isSelected)
    {
        var myEvent = isSelected ? OnSelected : OnDiselected;
        myEvent.Invoke();
    }
}

[System.Serializable]
public struct EntityStats
{
    public SoundSO audio;
    [Space]
    public int Cost;
    public int Radius;
    public ShapeType Shape;
}
