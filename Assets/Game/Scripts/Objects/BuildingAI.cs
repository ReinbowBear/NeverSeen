using UnityEngine;

public class BuildingAI : MonoBehaviour
{
    public ICondition[] conditions;

    private IBehavior[] Behaviours;

    void Start()
    {
        Behaviours = GetComponents<IBehavior>();
    }


    public void SetActive(bool isActive)
    {
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
