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
    public class OnCharacterInit : EventArgs 
    {
        public readonly Character character;
        public OnCharacterInit(Character newCharacter)
        {
            character = newCharacter;
        }
    }

    public class OnEnemyInit : EventArgs 
    {
        public readonly Enemy enemy;
        public OnEnemyInit(Enemy newEnemy)
        {
            enemy = newEnemy;
        }
    }
}
