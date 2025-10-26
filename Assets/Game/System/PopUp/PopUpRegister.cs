using UnityEngine;
using Zenject;

public class PopUpRegister : MonoBehaviour
{
    [Inject] private GameObject popUpCanvas;

    void Awake()
    {
        Register();
    }


    public void Register()
    {
        if (TryGetComponent<IHaveBar>(out var haveBar))
        {

        }

        if (TryGetComponent<IHaveNumber>(out var haveNumber))
        {

        }
    }
}


public class PopUpData
{
    public PopUpType popUpType;
    public float ShowTime;
}

public enum PopUpType // по нему должно будет определятся куда вставить поп апп вообще
{
    None
}
