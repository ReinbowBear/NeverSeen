using UnityEngine;

public class EnHealth : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private BarChange hpBar;

    void Start()
    {
        hpBar.ChangeBar(enemy.baseStats.health, enemy.stats.health);
    }


    public void TakeDamage(int damage)
    {
        enemy.stats.health -= damage * enemy.stats.takeDamageScale;
        hpBar.ChangeBar(enemy.stats.health, enemy.stats.health);

        if (enemy.stats.health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Content.DestroyAsset(gameObject);
    }
}