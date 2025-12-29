using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    private Camera cam;

    public EnumPanel buildPanel;

    private Dictionary<ViewMode, IViewMode> states;
    public IViewMode CurrentState { get; private set; }

    private Input input;

    void Awake()
    {
        cam = Camera.main;
    
        //states = new Dictionary<ViewMode, IViewMode>
        //{
        //    { ViewMode.view, factory.GetClass<DefaultMode>((LayerMask)LayerMask.GetMask("Tile")) },
        //    { ViewMode.edit, factory.GetClass<EditMode>((LayerMask)LayerMask.GetMask("Tile"), this) }
        //};

        SetMode(ViewMode.view);
    }


    public void SetState(string stateName)
    {
        
    }

    public void ToggleMode(int newMode)
    {
        if (CurrentState is DefaultMode)
        {
            SetMode((ViewMode)newMode);
        }
        else
        {
            SetMode(ViewMode.view);
        }
    }

    public void SetMode(ViewMode mode)
    {
        if (states.TryGetValue(mode, out var newState))
        {
            if (CurrentState == newState) return;
            CurrentState = newState;
            Debug.Log(CurrentState.GetType());
        }
    }


    private void LeftClick(InputAction.CallbackContext _) => StartCoroutine(DoLeftClick());
    private IEnumerator DoLeftClick()
    {
        Ray ray = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);

        yield return null;
        if (EventSystem.current.IsPointerOverGameObject()) yield break;
        if (!Physics.Raycast(ray, out RaycastHit hit, 30, CurrentState.GetRayLayer())) yield break;

        CurrentState.LeftClick(hit);
    }


    private void RightClick(InputAction.CallbackContext _) => StartCoroutine(TryRightClick());
    private IEnumerator TryRightClick()
    {
        Vector3 mousePos = UnityEngine.Input.mousePosition;

        while (input.GamePlay.MouseRight.IsPressed()) yield return null;

        if ((mousePos - UnityEngine.Input.mousePosition).magnitude <= 100f) // срабатывает если разница не более number float
        {
            CurrentState.RightClick();
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


public class ClickController
{
    private Input input;
    private Camera cam;

    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults;
    private LayerMask layerMask;

    public ClickController(Input input)
    {
        this.input = input;
        cam = Camera.main;

        pointerEventData = new PointerEventData(EventSystem.current);
        raycastResults = new();
    }


    public void LeftClick(InputAction.CallbackContext _)
    {
        if (IsPointerOverUI()) return;

        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out var hit, 30, layerMask, QueryTriggerInteraction.Ignore))
        {
            //Select(hit.collider.gameObject);
        }
    }

    public void RightClick(InputAction.CallbackContext _)
    {
        
    }

    public void SetLayer(int layer, bool enabled)
    {
        if (enabled) layerMask |= 1 << layer;
        else layerMask &= ~(1 << layer);
    }


    private bool IsPointerOverUI()
    {
        pointerEventData.position = Mouse.current.position.ReadValue();

        raycastResults.Clear();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        return raycastResults.Count > 0;
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
