using DG.Tweening;
using UnityEngine;

public class Health : MonoBehaviour
{
    [HideInInspector] public Entity character;
    public BarChange hpBar;

    public void TakeDamage(int damage, DamageType type)
    {
        if (type == DamageType.physics)
        {
            damage -= character.currentStats.armor;
        }

        if (damage < 0)
        {
            damage = 0;
        }

        character.currentStats.health -= damage;
        hpBar.ChangeBar(character.baseStats.health, character.currentStats.health);

        if (character.currentStats.health <= 0)
        {
            Death();
        }
        else
        {
            DOTween.Sequence()
                .SetLink(gameObject)
                .Append(transform.DOScale(new Vector3(0.95f, 1.1f, 0.95f), 0.25f))
                .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
        }
    }

    public void TakeHeal(int heal)
    {
        character.currentStats.health += heal;

        if (character.currentStats.health > character.baseStats.health)
        {
            character.currentStats.health = character.baseStats.health;
        }

        hpBar.ChangeBar(character.baseStats.health, character.currentStats.health);

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOScale(new Vector3(0.9f, 1.2f, 0.9f), 0.25f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
    }

    private void Death()
    {
        transform.parent = null;
        Address.DestroyAsset(gameObject);
    }
}
