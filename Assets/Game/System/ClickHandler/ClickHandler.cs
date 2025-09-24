using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

public class ClickHandler : MonoBehaviour
{
    private Camera cam;

    private Dictionary<Type, IViewMode> states = new();
    public IViewMode CurrentState { get; private set; }

    [Inject] private Input input;
    [Inject] private Factory factory;

    void Awake()
    {
        cam = Camera.main;

        states.Add(typeof(DefaultMode), factory.GetClass<DefaultMode>((LayerMask)LayerMask.GetMask("Entity"))); // без ручного преобразования зенджект жалуется
        states.Add(typeof(EditMode), factory.GetClass<EditMode>((LayerMask)LayerMask.GetMask("Tile"), this));

        SetMode<DefaultMode>();
    }


    public void SetMode<T>() where T : class, IViewMode
    {
        var type = typeof(T);
        CurrentState = states[type];
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
