using DG.Tweening;
using UnityEngine;

public class ShowGameUI : MonoBehaviour
{
    [SerializeField] private RectTransform[] Uirect;
    [SerializeField] private Vector2[] hidePos;

    private RectTransform[] ShowPos;

    void Awake()
    {
        ShowPos = new RectTransform[Uirect.Length];

        for (byte i = 0; i < Uirect.Length; i++)
        {
            ShowPos[i] = Uirect[i];
        }
    }


    public void ShowUI()
    {
        for (byte i = 0; i < Uirect.Length; i++)
        {
            Uirect[i].gameObject.SetActive(true);

            Uirect[i].DOAnchorPos(hidePos[i], 0.6f)
                .SetLink(Uirect[i].gameObject)
                .From();
        }
    }

    public void HideUI(MyEvent.OnEndLevel _)
    {
        for (byte i = 0; i < Uirect.Length; i++)
        {
            Uirect[i].gameObject.SetActive(false);
        }
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEndLevel>(HideUI);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEndLevel>(HideUI);
    }
}
