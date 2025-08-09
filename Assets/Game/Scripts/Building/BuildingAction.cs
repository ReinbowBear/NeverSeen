using UnityEngine;

public abstract class BuildingAction : MonoBehaviour
{
    [HideInInspector] public Building Owner;

    public virtual void Init() { }
    public abstract void Active(bool isActive);
    public virtual void Delete() { } // вообще на данный момент эта функция не очень нужна (есть енерджи кериер и енерджи юзер только с ней)
}
