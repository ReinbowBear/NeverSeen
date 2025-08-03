using UnityEngine;

public abstract class BuildingAction : MonoBehaviour
{
    [HideInInspector] public Building Owner;

    public virtual void Init() { }
    public abstract void Active();
    public abstract void Deactive();
    public virtual void Delete() { }
}
