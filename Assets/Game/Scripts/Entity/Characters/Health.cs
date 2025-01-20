using DG.Tweening;
using UnityEngine;

public class Health : MonoBehaviour
{
    [HideInInspector] public Entity character;
    public BarChange hpBar;

    public void TakeDamage(int damage, DamageType type)
    {
        int fullDamage = 100;
        switch (type)
        {
            case DamageType.melee:
                fullDamage = damage - character.currentStats.meleeArmor;
                break;
            case DamageType.range:
                fullDamage = damage - character.currentStats.rangeArmor;
                break;
            case DamageType.magic:
                fullDamage = damage - character.currentStats.magicArmor;
                break;
        }

        if (fullDamage < 0)
        {
            fullDamage = 0;
        }

        character.currentStats.health -= fullDamage;
        hpBar.ChangeBar(character.baseStats.health, character.currentStats.health);

        if (character.currentStats.health <= 0)
        {
            Death();
        }

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOScale(new Vector3(0.95f, 1.1f, 0.95f), 0.25f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
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
        Address.DestroyAsset(gameObject);
        transform.parent = null; // отвязываемся от позиции на карте, что бы та считалась свободной

        if (!character.baseStats.isPlayer)
        {
            EventBus.Invoke<MyEvent.OnEnemyDeath>();
        }
        else
        {
            EventBus.Invoke<MyEvent.OnEndLevel>();
        }
    }
}
