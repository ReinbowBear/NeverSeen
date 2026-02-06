using UnityEngine;

public class ShakeText : MonoBehaviour 
{
    public float magnitude = 0.1f;
    public float speed = 0.6f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private Vector3 currentPos;

    void Awake()
    {
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(currentPos, targetPos) < 0.01f)
        {
            float x = Random.Range(-magnitude, magnitude);
            float y = Random.Range(-magnitude, magnitude);
            targetPos = startPos + new Vector3(x, y, 0.0f);
        }

        currentPos = Vector3.Lerp(currentPos, targetPos, speed);
        transform.position = currentPos;
    }
}
