using System;

public static class MyEvent
{
    //Map Scene
    public class OnEntryMap : EventArgs { }
    public class OnNewBattle : EventArgs { }

    //Battle Scene
    public class OnEntryBattle : EventArgs { }
    public class OnEndLevel : EventArgs { }
}
