using System.Collections;

public class Sword : Weapon
{
    private Enemy lastEnemy;

    public override IEnumerator Attacking()
    {
        //StartCoroutine(Reload());
//
        //if (character.battleMap.enemys[0] != null)
        //{
        //    int damage = stats.damage;
//
        //    if (lastEnemy != character.battleMap.enemys[0])
        //    {
        //        damage = damage * 2;
        //        lastEnemy = character.battleMap.enemys[0];
        //    }
//
        //    character.battleMap.enemys[0].health.TakeDamage(stats.damage);
        //    yield return null;
        //}
        yield return null;
    }
}
