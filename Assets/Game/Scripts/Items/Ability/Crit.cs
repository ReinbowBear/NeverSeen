using System.Collections;

public class Crit : Ability
{
    private Enemy pastEnemy;

    protected override IEnumerator GetAttack(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.health.TakeDamage(stats.damage);

            if (enemy != pastEnemy)
            {
                enemy.health.TakeDamage(stats.damage);
                pastEnemy = enemy;
            }
        }
        yield return null;
        AttackCorutine = null;
    }
}
