using System.Collections;
using UnityEngine;

public class NavigateObject : MonoBehaviour
{
    [SerializeField] private RectTransform navigateObject;
    [SerializeField] private float navigateTime = 0.1f;
    [Space]
    [SerializeField] private Panel panel;


    public void OnButtonChose(MyButton button)
    {
        CoroutineManager.Start(MoveToButton(button.transform), this);
    }

    private IEnumerator MoveToButton(Transform target)
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


    void OnEnable()
    {
        foreach (var button in panel.Buttons)
        {
            button.OnButtonEnter += OnButtonChose;
        }
    }

    void OnDisable()
    {
        foreach (var button in panel.Buttons)
        {
            button.OnButtonEnter -= OnButtonChose;
        }
    }
}
