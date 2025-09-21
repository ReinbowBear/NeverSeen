using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

public class ClickHandler : MonoBehaviour
{
    private Camera cam;
    public StateMachine StateMachine { get; private set; } = new();

    private Input input;
    [SerializeField] private Factory factory;

    [Inject]
    public void Construct(Input input)
    {
        this.input = input;
    }

    void Awake()
    {
        cam = Camera.main;

        StateMachine.AddState(new DefaultMode(LayerMask.GetMask("Entity")));
        StateMachine.AddState(factory.GetClass<EditMode>(LayerMask.GetMask("Tile")));

        StateMachine.Start<DefaultMode>();
    }


    private void LeftClick(InputAction.CallbackContext _) => StartCoroutine(DoLeftClick());
    private IEnumerator DoLeftClick()
    {
        Ray ray = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);

        yield return null;
        if (EventSystem.current.IsPointerOverGameObject()) yield break;

        var currentMode = StateMachine.CurrentState as IViewMode;
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
            var currentMode = StateMachine.CurrentState as IViewMode;
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
