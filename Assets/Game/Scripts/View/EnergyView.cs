using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnergyView : MonoBehaviour, IEnergyView
{
    [SerializeField] private GameObject energyPoint;
    [SerializeField] public Transform wirePoint { get; private set; }

    private string wirePref = "Wire";
    private List<Wire> wires;

    private Factory objectFactory;

    [Inject]
    public void Construct(Factory objectFactory)
    {
        this.objectFactory = objectFactory;
        objectFactory.Register(wirePref).GetAwaiter().GetResult();
    }


    public void ShowEnergyPoint(bool isActive)
    {
        energyPoint.SetActive(isActive);
    }

    public void DrawWireTo(IEnergyView IEnergyView)
    {
        if (IEnergyView is not EnergyView energyView) return;

        GameObject obj = objectFactory.Create(wirePref);
        Wire wire = obj.GetComponent<Wire>();

        obj.transform.SetParent(wirePoint);
        wire.StartPoint = wirePoint.position;
        wire.EndPoint = energyView.wirePoint.position;
        wire.SetLine();

        wires.Add(wire);
    }


    void OnEnable()
    {
        wires = new();
    }

    void OnDisable() // не хватает какого нибудь события или функции что бы соседи пересчитали наличие своих проводов, но это должна делать логика
    {
        foreach (var wire in wires)
        {
            Destroy(wire.gameObject);
        }
    }
}
