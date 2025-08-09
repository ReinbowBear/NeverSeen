using System.Collections;
using UnityEngine;

public class EntityMenu : MonoBehaviour
{
    public static EntityMenu Instance;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float openSpeed;
    [SerializeField] private Vector2 hiddenOffset;

    private Vector2 ShowPosition;
    private Entity entity;
    private bool isShow;

    private void Awake()
    {
        Instance = this;
        ShowPosition = rectTransform.anchoredPosition;

        rectTransform.anchoredPosition = ShowPosition + hiddenOffset;
    }


    public void ShowPanel(Entity newEntity)
    {
        entity = newEntity;

        if (!isShow) CoroutineManager.Start(SlideTo(ShowPosition, true), this);
    }

    public void HidePanel()
    {
        if (isShow) CoroutineManager.Start(SlideTo(ShowPosition + hiddenOffset, false), this);
    }


    private IEnumerator SlideTo(Vector2 targetPosition, bool showOrHide)
    {
        Vector2 start = rectTransform.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < openSpeed)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(start, targetPosition, elapsed / openSpeed);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = targetPosition;
        isShow = showOrHide;
    }


    #region ButtonsFunc
    public void DeleteEntity()
    {
        if (entity is Building building)
        {
            building.Delete();
            HidePanel();
        }
        else
        {
            Debug.Log("пора делать стейты сюда какие нибудь?");
        }
    }
    #endregion
}