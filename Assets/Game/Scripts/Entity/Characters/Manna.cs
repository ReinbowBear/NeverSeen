using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Manna : MonoBehaviour
{
    [HideInInspector] public Entity character;
    public BarChange mpBar;
    public Coroutine coroutine;

    public void TakeManna(byte mannaCost)
    {
        character.currentStats.manna -= mannaCost;
        mpBar.ChangeBar(character.baseStats.manna, character.currentStats.manna);

        DOTween.Sequence()
                .SetLink(gameObject)
                .Append(transform.DOScale(new Vector3(1.1f, 0.8f, 1.1f), 0.25f))
                .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));

        if (coroutine == null)
        {
            coroutine = StartCoroutine(MannaRegen());
        }
    }

    public IEnumerator MannaRegen()
    {
        while (character.currentStats.manna < character.baseStats.manna)
        {
            yield return new WaitForSeconds(1);

            character.currentStats.manna += character.currentStats.mannaRegen;
            mpBar.ChangeBar(character.baseStats.manna, character.currentStats.manna);
        }

        character.currentStats.manna = character.baseStats.manna;
        mpBar.ChangeBar(character.baseStats.manna, character.currentStats.manna);
        coroutine = null;
    }
}
