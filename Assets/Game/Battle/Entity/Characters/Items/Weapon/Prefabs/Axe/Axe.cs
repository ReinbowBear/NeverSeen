using System.Collections;

public class Axe : Weapon
{
    public override IEnumerator Attacking()
    {
        StartCoroutine(Reload());

        for (int i = 0; i < character.entityManager.enemys.Length; i++)
        {
            if (character.entityManager.enemys[i] != null)
            {
                character.entityManager.enemys[i].health.TakeDamage(stats.damage);
                yield return null;
            }
        }
    }
}
