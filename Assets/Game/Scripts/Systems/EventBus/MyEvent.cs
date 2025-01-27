using System;

public static class MyEvent
{
    // system
    public class OnSave : EventArgs { }
    public class OnLoad : EventArgs { }

    // map Scene
    public class OnEntryMap : EventArgs { }
    public class OnNewBattle : EventArgs { }

    // battle Scene
    public class OnEntryBattle : EventArgs { }
    public class OnEndLevel : EventArgs { }

    // gamelay
    public class OnEntityInit : EventArgs 
    {
        public readonly Entity entity;
        public OnEntityInit(Entity newEntity)
        {
            entity = newEntity;
        }
    }

    public class OnEnemyDeath : EventArgs { }
}
