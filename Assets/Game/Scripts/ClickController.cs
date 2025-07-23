using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour
{
    public static ClickController Instance;

    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask rayLayer;
    private ModeState modeState;

    [HideInInspector] public Building SelectedBuilding;
    [HideInInspector] public Building NewBuilding;

    void Awake()
    {
        Instance = this;
    }

    public void SetMode(ModeState newState) // сюда как будь то вписывается стейт машина но код пока слишком маленький что бы она реально была нужна
    {
        modeState = newState;

        if (NewBuilding != null)
        {
            Destroy(NewBuilding.gameObject);
            NewBuilding = null;
        }
    }

    private void LeftClick(InputAction.CallbackContext _) // костыль что бы не получать предупреждение от IsPointerOverGameObject
    {
        StartCoroutine(LeftClick2(_));
    }
    private IEnumerator LeftClick2(InputAction.CallbackContext _)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        yield return null;
        if (Physics.Raycast(ray, out RaycastHit hit, 30, rayLayer) && EventSystem.current.IsPointerOverGameObject() != true)
        {
            Tile tile = hit.transform.GetComponent<Tile>();

            if (SelectedBuilding != null) // если здание выделено, либо сбрасывает при промахе либо выделяет новое
            {
                SelectedBuilding.Unselected();
                SelectedBuilding = null;
            }

            if (NewBuilding != null) // если в руках есть здание, ставит здание, ничего не выделяет ни в коем случаи
            {
                if (NewBuilding.TryPlace(tile))
                {
                    NewBuilding = null;
                }
                else
                {
                    // подрагивание или прочий импакт
                }
                yield break;
            }

            if (tile.tileData.isTaken != null && tile.tileData.isTaken.TryGetComponent<Building>(out Building building)) // если пустота и кликаешь по зданию, выделение
            {
                building.OnSelected();
            }
        }
    }

    private void RightClick(InputAction.CallbackContext _)
    {
        if (NewBuilding != null)
        {
            StartCoroutine(TryDropBuilding());
        }
    }


    private IEnumerator TryDropBuilding()
    {
        Vector3 mousePos = Input.mousePosition;

        while (BattleKeyboard.gameInput.Player.MouseRight.IsPressed())
        {
            yield return null;
        }

        if (mousePos == Input.mousePosition)
        {
            Destroy(NewBuilding.gameObject);
            NewBuilding = null;
        }
    }


    void Start()
    {
        BattleKeyboard.gameInput.Player.MouseLeft.started += LeftClick;
        BattleKeyboard.gameInput.Player.MouseRight.started += RightClick;
    }

    void OnDisable()
    {
        BattleKeyboard.gameInput.Player.MouseLeft.started -= LeftClick;
        BattleKeyboard.gameInput.Player.MouseRight.started -= RightClick;
    }
}

public enum ModeState
{
    view, edit
}
