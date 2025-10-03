using System.Collections.Generic;
using UnityEngine;

public class ChainView : MonoBehaviour
{
    public GameObject ChainPref;
    public Transform ChainPoint; // связи крепятся сюда и подразуемвается что при уничтожении объекта они сами отсюда уничтожатся

    private Dictionary<ChainView, Chain> chainViews = new();


    public void DrawTo(Transform EndPos)
    {
        if (!EndPos.gameObject.TryGetComponent<ChainView>(out var chainView))
        {
            chainView = EndPos.gameObject.AddComponent<ChainView>();
            chainView.ChainPref = ChainPref;
            chainView.ChainPoint = EndPos;
        }

        GameObject obj = Instantiate(ChainPref);
        Chain chain = obj.GetComponent<Chain>();

        obj.transform.SetParent(ChainPoint);

        chain.StartPoint = ChainPoint.position;
        chain.EndPoint = chainView.ChainPoint.position;
        chain.SetLine();

        chainView.AddConnect(this, chain);
    }


    public void AddConnect(ChainView chainView, Chain chain)
    {
        if (chainViews.ContainsKey(chainView)) return;

        chainViews.Add(chainView, chain);
    }

    public void RemoveConnect(ChainView chainView)
    {
        Destroy(chainViews[chainView].gameObject);
        chainViews.Remove(chainView);
    }


    void OnDestroy()
    {
        foreach (var chainView in chainViews.Keys)
        {
            chainView.RemoveConnect(this);
        }
    }
}
