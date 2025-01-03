using System;

public static class MyEvent
{
    //Map Scene
    public class OnEntryMap : EventArgs { }
    public class OnNewBattle : EventArgs { }

    //Battle Scene
    public class OnEntryBattle : EventArgs { }
    public class OnStartBattle : EventArgs { }
    public class OnEndLevel : EventArgs { }

    //gamelay
    public class OnEntityInit : EventArgs 
    {
        public readonly Entity entity;
        public OnEntityInit(Entity newEntity)
        {
            entity = newEntity;
        }
    }
}
