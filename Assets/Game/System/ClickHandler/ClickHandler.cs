using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

public class ClickHandler : MonoBehaviour
{
    private Camera cam;

    public IViewMode DefaultMode { get; private set; }
    public IViewMode editMode { get; private set; }
    private IViewMode currentMode;

    private Input input;
    private Factory factory;

    [Inject]
    public void Construct(Input input, Factory factory) // EditMode на кой то хрен забинжен в зенджекте, хотя нужен только тут!
    {
        this.input = input;
        this.factory = factory;
    }

    void Awake()
    {
        cam = Camera.main;

        DefaultMode = new DefaultMode(LayerMask.GetMask("Entity"));

        EditMode edit = factory.CreateClass<EditMode>();
        edit.Init(LayerMask.GetMask("Tile"));
        editMode = edit;

        currentMode = DefaultMode;
    }


    public void SetMode(ViewMode newState)
    {
        currentMode.RightClick();

        switch (newState)
        {
            case ViewMode.view:
                currentMode = DefaultMode;
                break;
            case ViewMode.edit:
                currentMode = editMode;
                break;
        }
    }


    private void LeftClick(InputAction.CallbackContext _) => StartCoroutine(DoLeftClick());

    private IEnumerator DoLeftClick()
    {
        Ray ray = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);

        yield return null;
        if (EventSystem.current.IsPointerOverGameObject()) yield break;

        if (!Physics.Raycast(ray, out RaycastHit hit, 30, currentMode.GetRayLayer())) yield break;

        currentMode.LeftClick(hit);
    }


    private void RightClick(InputAction.CallbackContext _) => StartCoroutine(TryRightClick());

    private IEnumerator TryRightClick()
    {
        Vector3 mousePos = UnityEngine.Input.mousePosition;

        while (input.GamePlay.MouseRight.IsPressed()) yield return null;

        if ((mousePos - UnityEngine.Input.mousePosition).magnitude <= 100f) // срабатывает если разница не более number float
        {
            currentMode.RightClick();
        }
    }


    void Start()
    {
        input.GamePlay.MouseLeft.started += LeftClick;
        input.GamePlay.MouseRight.started += RightClick;
    }

    void OnDestroy()
    {
        input.GamePlay.MouseLeft.started -= LeftClick;
        input.GamePlay.MouseRight.started -= RightClick;
    }
}

public enum ViewMode
{
    view, edit
}
