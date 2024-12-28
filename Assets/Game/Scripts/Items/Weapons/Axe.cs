using System.Collections;

public class Axe : Weapon
{
    public override IEnumerator Attacking()
    {
        //StartCoroutine(Reload());
//
        //for (int i = 0; i < character.battleMap.enemys.Length; i++)
        //{
        //    if (character.battleMap.enemys[i] != null)
        //    {
        //        character.battleMap.enemys[i].health.TakeDamage(stats.damage);
        //        yield return null;
        //    }
        //}
        yield return null;
    }
}
