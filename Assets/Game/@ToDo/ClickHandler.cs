using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    public static ClickHandler Instance;

    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask rayLayer;

    private ModeState modeState;
    private Entity SelectedEntity;
    [HideInInspector] public Spawned NewBuilding;

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
    private void OnLeftClick(InputAction.CallbackContext _) => StartCoroutine(DoLeftClick(_));

    private IEnumerator DoLeftClick(InputAction.CallbackContext _)
    {
        Ray ray = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);
        yield return null;

        if (!Physics.Raycast(ray, out RaycastHit hit, 30, rayLayer)) yield break;
        if (EventSystem.current.IsPointerOverGameObject()) yield break;

        Tile tile = hit.transform.GetComponent<Tile>();

        UnselectEntity();
        if (PlaceBuilding(tile)) yield break;
        SelectEntity(tile);
    }


    private void SelectEntity(Tile tile)
    {
        if (tile.tileData.IsTaken != null)
        {
            SelectedEntity = tile.tileData.IsTaken;
            SelectedEntity.Selected(true);
        }
    }

    private void UnselectEntity()
    {
        if (SelectedEntity != null)
        {
            SelectedEntity.Selected(false);
            SelectedEntity = null;
        }
    }

    private bool PlaceBuilding(Tile tile)
    {
        if (NewBuilding != null && NewBuilding.TryPlace(tile))
        {
            NewBuilding = null;
            return true;
        }
        // else подрагивание, визуальный фидбек
        return false;
    }
    #endregion

    #region RightClick
    private void OnRightClick(InputAction.CallbackContext _)
    {
        if (NewBuilding != null) StartCoroutine(TryDropBuilding());
    }

    private IEnumerator TryDropBuilding()
    {
        Vector3 mousePos = UnityEngine.Input.mousePosition;

        while (Input.Instance.GameInput.Gameplay.MouseRight.IsPressed())
        {
            yield return null;
        }

        if (mousePos == UnityEngine.Input.mousePosition)
        {
            NewBuilding.Selected(false);
            EntityMenu.Instance.HidePanel();
            Destroy(NewBuilding.gameObject);
            NewBuilding = null;
        }
    }
    #endregion


    void Start()
    {
        Input.Instance.GameInput.Gameplay.MouseLeft.started += OnLeftClick;
        Input.Instance.GameInput.Gameplay.MouseRight.started += OnRightClick;
    }

    void OnDestroy()
    {
        Input.Instance.GameInput.Gameplay.MouseLeft.started -= OnLeftClick;
        Input.Instance.GameInput.Gameplay.MouseRight.started -= OnRightClick;
    }
}

public enum ModeState
{
    view, edit
}
