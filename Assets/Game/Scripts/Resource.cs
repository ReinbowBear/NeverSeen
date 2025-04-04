using TMPro;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public static Resource instance;

    [SerializeField] private TextMeshProUGUI[] texts;
    [HideInInspector] public short money;

    void Awake()
    {
        instance = this;
    }


    public void RefreshUI()
    {

    }


    private void Load(OnLoad _)
    {

    }

    private void Save(OnSave _)
    {

    }


    void OnEnable()
    {
        EventBus.Add<OnLoad>(Load);
        EventBus.Add<OnSave>(Save);
    }

    void OnDisable()
    {
        EventBus.Remove<OnLoad>(Load);
        EventBus.Remove<OnSave>(Save);
    }
}
