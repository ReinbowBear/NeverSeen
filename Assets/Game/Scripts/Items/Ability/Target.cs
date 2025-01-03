using UnityEngine;

public class Target
{
    public virtual Entity[] GetTarget(Transform[] mapPoints)
    {
        Entity[] enemies = new Entity[mapPoints.Length];
        for (byte i = 0; i < mapPoints.Length; i++)
        {
            if (mapPoints[i].childCount != 0)
            {
                Entity enemy = mapPoints[i].GetComponentInChildren<Entity>();
                enemies[i] = enemy;
                break;
            }
        }
        return enemies;
    }
}
