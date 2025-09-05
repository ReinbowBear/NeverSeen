using UnityEngine;

public class Building : Entity, IBehavior
{
    public void SetActive(bool isActive)
    {
        foreach (var behaviour in Behaviours)
        {
            behaviour.SetActive(isActive);
        }

        if (!isActive) return;

        TweenAnimation.Impact(transform);
    }

    protected override void OnDelete()
    {
        base.OnDelete();
        SetActive(false);
    }
}
