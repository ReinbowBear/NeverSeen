using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EditView : MonoBehaviour
{
    private Camera cam;
    private List<Tile> tilesInRadius = new();

    [SerializeField] private ClickHandler clickHandler;
    private EditMode editMode;
    private GameMapData mapData;

    [Inject]
    public void Construct(GameData gameData)
    {
        this.mapData = gameData.GameMap;
    }

    void Start()
    {
        cam = Camera.main;
        editMode = clickHandler.editMode as EditMode;
    }


    public void OnSpawnBuilding() => CoroutineManager.Start(MoveBuilding(), this);
    public IEnumerator MoveBuilding()
    {
        Tile oldTile = null;
        Tile newTile;

        Building building = editMode.Building;
        tilesInRadius.Clear();

        while (building != null)
        {
            Ray ray = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, 30, editMode.GetRayLayer())) yield return null;

            building.transform.position = hit.transform.position;
            newTile = hit.transform.gameObject.GetComponent<Tile>();

            if (newTile == oldTile) yield return null;

            SetTileslight(false);
            tilesInRadius = mapData.GetTilesInRadius(newTile.tileData.CubeCoord, building.Stats.Radius);
            SetTileslight(true);

            oldTile = newTile;
            yield return null;
        }
        SetTileslight(false);
    }

    private void SetTileslight(bool isTrue)
    {
        foreach (var tile in tilesInRadius)
        {
            tile.SetBacklight(isTrue);
        }
    }


    void OnEnable()
    {
        editMode.OnNewBuilding += OnSpawnBuilding;
    }

    void OnDisable()
    {
        editMode.OnNewBuilding -= OnSpawnBuilding;
    }
}
