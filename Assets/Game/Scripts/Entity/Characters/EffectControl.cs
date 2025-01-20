using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EffectControl : MonoBehaviour
{
    [SerializeField] private Entity character;
    [SerializeField] private Transform effectPoint;
    private Dictionary<Effect, BarChange> effectsBar = new Dictionary<Effect, BarChange>();

    public async Task AddEffect(Effect newEffect)
    {
        if (!effectsBar.ContainsKey(newEffect))
        {
            GameObject effectBar = await Address.GetAssetByName("EffectBar");
            effectBar.transform.SetParent(effectPoint, false);
            effectBar.transform.localPosition = new Vector3(0, effectsBar.Count * 20, 0);

            effectsBar[newEffect] = effectBar.GetComponent<BarChange>();
            effectsBar[newEffect].icon.sprite = newEffect.stats.image;
        }
        else
        {
            effectsBar[newEffect].StopAllCoroutines();
        }

        if (!newEffect.stats.isUpdate)
        {
            effectsBar[newEffect].StartCoroutine(OneTime(newEffect));
        }
        else
        {
            effectsBar[newEffect].StartCoroutine(UpdateTime(newEffect)); //запускаю корутину в монобихейворе еффект бара, что бы в случаи чего её можно было стопать
        }
    }

    public void RemoveEffect(Effect oldEffect)
    {
        oldEffect.FalseEffect(character);

        Address.DestroyAsset(effectsBar[oldEffect].gameObject);
        effectsBar.Remove(oldEffect);
    }


    public IEnumerator OneTime(Effect myEffect)
    {
        myEffect.DoEffect(character);

        BarChange effectBar = effectsBar[myEffect];
        float timeToEnd = myEffect.stats.duration;

        while (timeToEnd > 0)
        {
            effectBar.ChangeBar(myEffect.stats.duration, timeToEnd);

            timeToEnd -= 1f;
            yield return new WaitForSeconds(1);
        }
        RemoveEffect(myEffect);
    }

    public IEnumerator UpdateTime(Effect myEffect)
    {
        BarChange effectBar = effectsBar[myEffect];
        
        float timeToEnd = myEffect.stats.duration;
        while (timeToEnd > 0)
        {
            myEffect.DoEffect(character);
            effectBar.ChangeBar(myEffect.stats.duration, timeToEnd);

            timeToEnd -= 1;
            yield return new WaitForSeconds(1);
        }
        RemoveEffect(myEffect);
    }
}
