using DG.Tweening;
using UnityEngine;

public class Storage : BuildingAction
{
    [SerializeField] private short Size;
    public override void Active(bool isActive)
    {
        if (isActive)
        {
            ResourceManager.Instance.AddLimit(Size);
        }
        else
        {
             ResourceManager.Instance.RemoveLimit(Size);
        }
    }


    public void AddResources(ResourceType type, short value)
    {
        ResourceManager.Instance.RefreshResource(type, value);

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOScale(new Vector3(0.95f, 1.1f, 0.95f), 0.25f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
    }
}
