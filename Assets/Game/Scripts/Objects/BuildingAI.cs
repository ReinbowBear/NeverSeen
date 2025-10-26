using UnityEngine;
using UnityEngine.Events;

public class BuildingAI : MonoBehaviour
{
    public UnityEvent<bool> OnActive;

    private ICondition[] conditions;
    private IBehavior[] Behaviours;

    void Awake()
    {
        conditions = GetComponents<ICondition>();
        Behaviours = GetComponents<IBehavior>();
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

        OnActive.Invoke(isActive);

        if (!isActive) return;

        //Tween.Impact(transform);
    }
}
