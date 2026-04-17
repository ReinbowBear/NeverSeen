using System.Collections.Generic;
using UnityEngine;

public class PanelNavigate : ISystem
{
    private List<Navigator> navigators = new(); // сущность может удалится! и данные будут неактуальны

    public void SetSubs(SystemSubs subs)
    {
        subs.AddListener<CurrentPanel, Navigator>(StartMove).OnEvent<OnNavigate>();
        subs.AddListener(ContinueMove);
    }


    private void StartMove(CurrentPanel panel, Navigator navigate)
    {
        navigate.StartPos = navigate.NavigateObj.position;
        navigate.EndPos = new Vector3(navigate.StartPos.x, navigate.StartPos.y, navigate.StartPos.z); // анимация навигации идёт только по Y!
        navigate.Elapsed = 0f;

        navigators.Add(navigate);
    }


    private void ContinueMove()
    {
        for (int i = navigators.Count - 1; i >= 0; i--)
        {
            var nav = navigators[i];

            if(nav.Elapsed < nav.NavigateTime)
            {
                nav.NavigateObj.position = Vector3.Lerp(nav.StartPos, nav.EndPos, nav.Elapsed / nav.NavigateTime);
                nav.Elapsed += Time.unscaledDeltaTime;
            }
            else
            {
                navigators.RemoveAt(i);
            }
        }
    }
}
