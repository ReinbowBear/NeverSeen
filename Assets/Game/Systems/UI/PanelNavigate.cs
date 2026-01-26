using System.Collections;
using UnityEngine;

public class PanelNavigate : MonoBehaviour
{
    public RectTransform NavigateObj;
    public float navigateTime = 0.1f;

    public void MoveTo(Vector3 position)
    {
        StopAllCoroutines();
        StartCoroutine(DoMove(position));
    }

    private IEnumerator DoMove(Vector3 position)
    {
        Vector3 startPos = NavigateObj.position;
        Vector3 endPos = new Vector3(startPos.x, position.y, startPos.z); // анимация навигации идёт только по Y!

        float timeElapsed = 0f;

        while (timeElapsed < navigateTime)
        {
            NavigateObj.position = Vector3.Lerp(startPos, endPos, timeElapsed / navigateTime);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        NavigateObj.position = endPos;
    }
}
