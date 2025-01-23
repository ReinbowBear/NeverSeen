using System.Collections;
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

        if (coroutine == null)
        {
            coroutine = StartCoroutine(MannaRegen());
        }
    }

    private IEnumerator MannaRegen()
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
