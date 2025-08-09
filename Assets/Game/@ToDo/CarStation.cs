using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarStation : BuildingAction
{
    [SerializeField] private GameObject carPref;
    private GameObject car;
    [SerializeField] private float carSpeed;

    private HashSet<Transform> pathPoints = new();
    private HashSet<Transform> reversPoints;

    public override void Init()
    {
        car = Instantiate(carPref, transform);
    }

    public override void Active(bool isActive)
    {
        if (isActive)
        {
            RefreshPath();
            CoroutineManager.Start(GoCar(), this);
        }
        else
        {
            CoroutineManager.Stop(GoCar(), this);
        }
    }

    public void RefreshPath()
    {
        pathPoints.Clear();

        Tile currentTile = Owner.Tile;
        bool isFoundRoad = true;

        while (isFoundRoad == true)
        {
            var tiles = Owner.GetTilesInRadius(currentTile.tileData.CubeCoord, 1);
            isFoundRoad = false;

            foreach (var tile in tiles)
            {
                if (tile.tileData.IsTaken is Building building && building.TryGetComponent(out Road road))
                {
                    if (pathPoints.Contains(road.transform)) continue;

                    pathPoints.Add(road.transform);
                    currentTile = road.Tile;
                    isFoundRoad = true;
                    break;
                }
            }
        }

        reversPoints = new HashSet<Transform>(pathPoints);
        reversPoints.Reverse();
    }


    public IEnumerator GoCar() // не сохраняет текущее состояние поездки, начнёт ехать заново при перевключении
    {
        while (true)
        {
            yield return StartCoroutine(CarRide(pathPoints));
            yield return StartCoroutine(CarRide(reversPoints));
        }
    }

    public IEnumerator CarRide(HashSet<Transform> path)
    {
        foreach (Transform targetPoint in path)
        {
            car.transform.LookAt(targetPoint.position);
            while (Vector3.Distance(car.transform.position, targetPoint.position) > 0.01f)
            {
                car.transform.position = Vector3.MoveTowards(car.transform.position, targetPoint.position, carSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
