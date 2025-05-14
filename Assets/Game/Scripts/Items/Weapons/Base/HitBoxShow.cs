using UnityEngine;

public class HitBoxShow : MonoBehaviour
{
    [SerializeField] private CircleHitBox circleHitBox;
    [SerializeField] private Color rayColor;

    void Update()
    {
        for (byte i = 0; i <= circleHitBox.points; i++)
        {
            float angle = Mathf.Lerp(0, circleHitBox.radius, i / (float)circleHitBox.points);
            float rad = angle * Mathf.Deg2Rad;

            Vector3 dir = new Vector3(Mathf.Cos(rad), 0f, Mathf.Sin(rad));
            Debug.DrawRay(transform.position, dir * circleHitBox.range, rayColor);
        }
    }
}
