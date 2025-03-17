using DG.Tweening;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private CharacterData character;
    public BarChange hpBar;

    public void TakeDamage(short damage)
    {
        character.hp -= damage;
        hpBar.ChangeBar(character.maxHp, character.hp);

        if (character.hp <= 0)
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

    public void TakeHeal(short heal)
    {
        character.hp += heal;

        if (character.hp > character.maxHp)
        {
            character.hp = character.maxHp;
        }

        hpBar.ChangeBar(character.maxHp, character.hp);

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOScale(new Vector3(0.9f, 1.2f, 0.9f), 0.25f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
    }

    private void Death()
    {
        Address.DestroyAsset(gameObject);
    }
}
