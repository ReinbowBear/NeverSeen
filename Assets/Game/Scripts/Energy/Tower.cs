using System.Collections;
using UnityEngine;

public class Tower : EnergyCarrier
{
    private string WireAddres = "Wire";

    public override void Active()
    {
        base.Active();
        StartCoroutine(SetWires());
    }


    private IEnumerator SetWires()
    {
        var handle = Loader.LoadAssetAsync<GameObject>(WireAddres);
        yield return new WaitUntil(() => handle.IsCompleted);

        foreach (var neighbor in connections)
        {
            GameObject obj = Instantiate(handle.Result, wirePoint);
            Wire component = obj.GetComponent<Wire>();

            component.StartPoint = wirePoint.position;
            component.EndPoint = neighbor.wirePoint.position;
            component.SetLine();
        }
    }
}
