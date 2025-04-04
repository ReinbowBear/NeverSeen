using System;
using DG.Tweening;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action onDamageTake;

    public BarChange hpBar;
    [Space]
    public short maxHp;
    private short hp;

    void Start()
    {
        hp = maxHp;
        hpBar.ChangeBar(maxHp, hp);
    }

    public void TakeDamage(short damage)
    {
        hp -= damage;
        hpBar.ChangeBar(maxHp, hp);
        onDamageTake?.Invoke();

        if (hp <= 0)
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
        hp += heal;

        if (hp > maxHp)
        {
            hp = maxHp;
        }

        hpBar.ChangeBar(maxHp, hp);

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOScale(new Vector3(0.9f, 1.2f, 0.9f), 0.25f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
