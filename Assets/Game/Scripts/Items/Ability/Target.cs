using UnityEngine;

public class Target
{
    public virtual Enemy[] GetTarget(BattleMap map)
    {
        //for (byte i = 0; i < map.Length; i++)
        //{
        //    if (map[i].childCount != 0)
        //    {
        //        Enemy enemy = map[i].GetComponentInChildren<Enemy>();
        //        return enemy;
        //    }
        //}
        Enemy[] enemies = new Enemy[map.enemyPoints.Length];
        return null;
    }
}
