using UnityEngine;
public class AngryEffect : TextEffect
{
    public float Magnitude = 2f;

    public AngryEffect( GameObject gameObject ) : base( gameObject )
    {
        // зачем это тут
    }

    public override void Update()
    {
        float x = Random.Range( -Magnitude, Magnitude );
        float y = Random.Range( -Magnitude, Magnitude );
        gameObject.transform.localPosition = m_startPos + new Vector3( x, y, 0.0f );
    }
}
