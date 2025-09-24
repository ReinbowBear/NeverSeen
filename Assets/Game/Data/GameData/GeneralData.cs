
using System;
using Zenject;

[System.Serializable]
public class GeneralData
{
    public bool IsGameInit;
    public byte SceneIndex;
    public int Seed;
}

public class GlobalData : IInitializable, IDisposable
{
    public void Initialize()
    {
        EventBus.AddSubscriber<OnSave>(Save);
        EventBus.AddSubscriber<OnLoad>(Load);
    }

    private void Save(OnSave onSave)
    {

    }

    private void Load(OnLoad onLoad)
    {

    }

    public void Dispose()
    {
        EventBus.RemoveSubscriber<OnSave>(Save);
        EventBus.RemoveSubscriber<OnLoad>(Load);
    }
}