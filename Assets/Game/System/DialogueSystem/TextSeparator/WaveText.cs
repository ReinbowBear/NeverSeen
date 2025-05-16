using UnityEngine;

public class WaveText : MonoBehaviour
{
    public float Frequency = 6;
    public float Magnitude = 0.1f;
    public float Offset; // не знаю что делает эта переменная, надо смотреть в синусе ниже

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void Update()
    {
        float y = Mathf.Sin((Offset + Time.time) * Frequency) * Magnitude;
        transform.localPosition = startPos + new Vector3(0.0f, y, 0.0f);
    }
}