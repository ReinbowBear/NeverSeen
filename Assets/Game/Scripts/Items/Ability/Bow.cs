
public class Bow : Ability
{
    protected override Enemy GetEnemy(int _)
    {
        if (character.battleMap.enemyPoints[1].childCount > 0)
        {
            Enemy enemy = character.battleMap.enemyPoints[1].GetComponent<Enemy>();
            return enemy;
        }
        else if (character.battleMap.enemyPoints[0].childCount > 0)
        {
            Enemy enemy = character.battleMap.enemyPoints[0].GetComponent<Enemy>();
            return enemy;
        }
        return null;
    }
}
