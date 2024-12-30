
public class FireBall : Ability
{
    public override void Activate()
    {
        for (byte i = 0; i < character.battleMap.enemyPoints.Length; i++)
        {
            Enemy enemy = GetEnemy(i);
            AttackCorutine = StartCoroutine(GetAttack(enemy));
        }

        StartCoroutine(GetReload());
    }
}
