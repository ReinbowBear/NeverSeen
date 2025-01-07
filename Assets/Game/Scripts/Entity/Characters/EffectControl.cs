using UnityEngine;

public class EffectControl : MonoBehaviour
{
    [SerializeField] private Entity myCharacter;
    private Coroutine effect;

    public void AddEffect(BaseEffect newEffect)
    {
        if (effect != null)
        {
            StopCoroutine(effect);
        }
        effect = StartCoroutine(newEffect.EffectCoroutine(myCharacter));
    }
}
