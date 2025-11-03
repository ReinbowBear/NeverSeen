using UnityEngine;
using UnityEngine.Events;

public class EntityOld : MonoBehaviour
{
    public UnityEvent OnSelected;
    public UnityEvent OnDiselected;

    public int Cost;
    public ShapeType Shape;


    public void Selected(bool isSelected)
    {
        var myEvent = isSelected ? OnSelected : OnDiselected;
        myEvent.Invoke();
    }


    void OnDisable()
    {
        Selected(false);
    }
}
