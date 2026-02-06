using UnityEngine;

public class WaveEffect : TextEffect
{
    public float Frequency = 8.0f;
    public float Magnitude = 1.5f;
    public float Offset;

    public WaveEffect( GameObject gameObject ) : base( gameObject )
    {
        // зачем это тут
    }

    void Start()
    {
        startPos = gameObject.transform.localPosition;
    }

    public override void Update()
    {
        float y = Mathf.Sin( ( Offset + Time.time ) * Frequency ) * Magnitude;
        gameObject.transform.localPosition = startPos + new Vector3( 0.0f, y, 0.0f );
    }
}
