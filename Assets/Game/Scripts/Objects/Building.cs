using UnityEngine;

public class Building : Spawned, IBehavior
{
    public ConfigHandler Config { get; private set; }

    public void SetActive() { SetActive(true); }
    public void SetActive(bool isActive)
    {
        foreach (var behaviour in Config.Behaviours)
        {
            behaviour.SetActive(isActive);
        }

        if (!isActive) return;

        TweenAnimation.Impact(transform);
    }


    void OnEnable()
    {
        OnSpawned += SetActive;
    }

    void OnDisable()
    {
        Selected(false);
        SetActive(false);

        OnSpawned -= SetActive;
    }
}
