using System;
using UnityEngine;

public class ProxyInput : MonoBehaviour, IDisposable
{
    public InputMode StartedMode;
    private Input Input = new();

    private EventWorld eventWorld;

    public ProxyInput()
    {
        Input.Init(eventWorld);
        Input.SwitchTo(StartedMode);
    }


    public void Dispose()
    {
        Input.Dispose();
    }
}
