using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    public virtual void OnEnable()
    {
        EventReflection.SubscribeClass(this, true);
    }

    public virtual void OnDisable()
    {
        EventReflection.SubscribeClass(this, false);
    }
}
