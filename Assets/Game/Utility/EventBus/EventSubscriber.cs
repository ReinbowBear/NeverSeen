using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    [SerializeField] private MonoBehaviour myClass;

    protected virtual void Awake()
    {
        if (myClass == null && this.GetType() != typeof(EventSubscriber))
        {
            myClass = this;
        }
    }

    public virtual void OnEnable()
    {
        EventReflection.SubscribeClass(myClass, true);
    }

    public virtual void OnDisable()
    {
        EventReflection.SubscribeClass(myClass, false);
    }
}
