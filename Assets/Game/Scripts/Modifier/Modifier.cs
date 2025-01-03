using UnityEngine;

public abstract class Modifier : MonoBehaviour
{
    [SerializeField] protected float value;
    [SerializeField] protected float duration;

    public virtual void Active(Entity entity)
    {
        Debug.Log("нету реализации!");
    }

    public virtual void Deactivate()
    {
        Debug.Log("нету реализации!");
    }
}
