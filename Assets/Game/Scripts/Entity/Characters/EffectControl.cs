using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{
    [HideInInspector] public Entity entity;
    [HideInInspector] public List<Effect> effects = new List<Effect>();

    public void AddEffect(Effect newEffect)
    {
        effects.Add(newEffect);
        StartCoroutine(newEffect.DoEffect(entity));
    }
}
