using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private BarChange hpBar;

    void Start()
    {
        hpBar.ChangeBar(character.baseStats.health, character.stats.health);
    }


    public void TakeDamage(int damage)
    {
        character.stats.health -= damage * character.stats.takeDamageScale;
        hpBar.ChangeBar(character.stats.health, character.stats.health);

        if (character.stats.health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Content.DestroyAsset(gameObject);
    }
}
