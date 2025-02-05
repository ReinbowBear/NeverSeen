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
        if (effectsBar.ContainsKey(newEffect) == false)
        {
            GameObject effectBar = await Address.GetAssetByName("EffectBar");
            BarChange barScript = effectBar.GetComponent<BarChange>();

            barScript.image.sprite = newEffect.stats.image;

            effectBar.transform.SetParent(effectPoint, false);
            effectBar.transform.localPosition = new Vector3(0, effectsBar.Count * 20, 0);

            effectsBar[newEffect] = barScript;
        }
        else
        {
            effectsBar[newEffect].StopAllCoroutines();
        }

        if (newEffect.stats.isUpdate == false)
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
        
        UpdateEffectsUI();
    }


    private void UpdateEffectsUI()
    {
        byte pos = 0;
        foreach (BarChange bar in effectsBar.Values)
        {
            bar.transform.localPosition = new Vector3(0, pos * 20, 0);
            pos++;
        }
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
