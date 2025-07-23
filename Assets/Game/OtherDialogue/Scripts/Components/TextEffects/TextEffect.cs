using System.Collections.Generic;
using UnityEngine;

public abstract class TextEffect
{
    public static Dictionary<string, ITextEffectBuilder> effects = new Dictionary<string, ITextEffectBuilder>
    {
        { "normal",  null },
        { "angry",   new AngryEffectBuilder()},
        { "wave",    new WaveEffectBuilder()}
    };

    public Vector3 startPos;
    public GameObject gameObject;

    public TextEffect(GameObject newObject )
    {
        gameObject = newObject;
        startPos = newObject.transform.localPosition;
    }

    public abstract void Update();
}