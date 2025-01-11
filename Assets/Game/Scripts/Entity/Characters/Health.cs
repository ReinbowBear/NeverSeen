using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Entity entity;
    [SerializeField] private BarChange hpBar;

    public void TakeDamage(int damage)
    {
        entity.stats.health -= damage;
        hpBar.ChangeBar(entity.stats.health, entity.stats.health);

        if (entity.stats.health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Address.DestroyAsset(gameObject);
    }
}
