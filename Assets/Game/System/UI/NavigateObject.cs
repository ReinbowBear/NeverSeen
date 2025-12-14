using System.Collections;
using UnityEngine;

public class NavigateObject : MonoBehaviour
{
    public RectTransform navigateObject;
    public float navigateTime = 0.1f;


    public void MoveTo(Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(DoMove(target));
    }

    private IEnumerator DoMove(Transform target)
    {
        Vector3 startPos = navigateObject.position;
        Vector3 endPos = new Vector3(startPos.x, target.position.y, startPos.z); // анимация навигации идёт только по Y!

        float timeElapsed = 0f;

        while (timeElapsed < navigateTime)
        {
            navigateObject.position = Vector3.Lerp(startPos, endPos, timeElapsed / navigateTime);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        navigateObject.position = endPos;
    }
}
