using System;
using UnityEngine;

public class ProxyInput : MonoBehaviour, IInitializable, IDisposable
{
    public InputMode StartedMode;
    private Input Input = new();

    private EventWorld eventWorld;

    public void Init()
    {
        Input.Init(eventWorld);
        Input.SwitchTo(StartedMode);
    }


    public void Dispose()
    {
        Input.Dispose();
    }
}
