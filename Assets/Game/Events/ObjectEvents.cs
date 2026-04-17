
namespace Events
{
    public enum ObjectEvents
    {
        Spawn, 
        Destroy,

        Activate,
        Deactivate,

        Select,
        Deselect,
    }
}

public struct SpawnEvent { }
public struct DestroyEvent { }

public struct ActivateEvent { }
public struct DeactivateEvent { }
