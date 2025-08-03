using DG.Tweening;
using UnityEngine;

public class Storage : Building
{
    public override void Active()
    {
        base.Active();
        //PlayerResource.Instance.MaxResource нужно там добавить функцию для обновления
    }

    public override void Deactive()
    {
        base.Deactive();
    }


    public void AddResources(ResourceType type, int value)
    {
        //PlayerResource.Instance.resources[type] += value;

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOScale(new Vector3(0.95f, 1.1f, 0.95f), 0.25f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
    }
}
