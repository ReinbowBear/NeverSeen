using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject model;
    [Space]
    [SerializeField] private float speed;
    [SerializeField] private float rotate;

    public void MoveTo(Vector2 direction)
    {
        if (character.state == CharacterState.attack)
        {
            return;
        }

        Vector3 newDirection = new Vector3(direction.x, 0, direction.y).normalized;
        rb.AddForce(newDirection * speed, ForceMode.Impulse);

        Quaternion targetRotation = Quaternion.LookRotation(newDirection);
        model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, targetRotation, rotate);
    }

    public IEnumerator RotateTo(Vector3 target, float duration)
    {
        Quaternion startRotation = model.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(target - model.transform.position);
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            model.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        model.transform.rotation = targetRotation;
    }
}
