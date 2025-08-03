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
    private Entity SelectedEntity;
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

    #region LeftClick
    private void LeftClick(InputAction.CallbackContext _)
    {
        StartCoroutine(DoLeftClick(_));
    }

    private IEnumerator DoLeftClick(InputAction.CallbackContext _)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        yield return null;

        if (!Physics.Raycast(ray, out RaycastHit hit, 30, rayLayer)) { yield break; }
        if (EventSystem.current.IsPointerOverGameObject()) { yield break; }

        Tile tile = hit.transform.GetComponent<Tile>();

        UnselectEntity();
        if (PlaceBuilding(tile) == true) { yield break; }
        SelectEntity(tile);
    }


    private void SelectEntity(Tile tile)
    {
        if (tile.tileData.IsTaken != null)
        {
            SelectedEntity = tile.tileData.IsTaken;
            SelectedEntity.Selected();
            EntityMenu.Instance.ShowPanel(SelectedEntity);
        }
    }

    private void UnselectEntity()
    {
        if (SelectedEntity != null)
        {
            SelectedEntity.Unselected();
            EntityMenu.Instance.HidePanel();
            SelectedEntity = null;
        }
    }

    private bool PlaceBuilding(Tile tile)
    {
        if (NewBuilding.TryPlace(tile))
        {
            NewBuilding = null;
            return true;
        }
        // else подрагивание, визуальный фидбек
        return false;
    }
    #endregion

    #region RightClick
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
            NewBuilding.Unselected();
            EntityMenu.Instance.HidePanel();
            Destroy(NewBuilding.gameObject);
            NewBuilding = null;
        }
    }
    #endregion


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
