using UnityEngine;
using Zenject;

public class BuildingAI : MonoBehaviour, IInitializable
{
    private ICondition[] conditions;
    private IBehavior[] Behaviours;

    public void Initialize()
    {
        GetComponents<IInitializable>().Initialize();
        conditions = GetComponents<ICondition>();
        Behaviours = GetComponents<IBehavior>();
        SetActive(true);
    }


    public void SetActive(bool isActive)
    {
        foreach (var condition in conditions)
        {
            if (!condition.IsConditionMet()) return;
        }

        foreach (var behaviour in Behaviours)
        {
            behaviour.SetActive(isActive);
        }

        if (!isActive) return;

        Tween.Impact(transform);
    }


    void OnDisable()
    {
        SetActive(false);
    }
}
