using UnityEngine;
using UnityEngine.UI;

public class PanelNavigator : ISystem
{
    // создаем объект таргет. навигатор к нему двигается. текущий навигатор отмечаем так же как панель но это разные сущности, разные системы?
    public void Execute(World world, EntityCommands commands)
    {
        foreach (var (button, transform) in world.Query<Button, RectTransform>().Require<OnHoverEnter>()) // кросс запрос получается, весьма запутанно!
        {
            foreach (var (panel, navigate, entity) in world.Query<CurrentPanel, Navigator>())
            {
                commands.AddComponent(entity, new RunTag());
                StartMove(navigate, transform.position);
            }
        }
        

        foreach (var (panel, navigate, entity) in world.Query<CurrentPanel, Navigator>().Require<RunTag>())
        {
            MoveNavigator(navigate);

            if(navigate.Elapsed > navigate.NavigateTime)
            {
                commands.RemoveComponent<RunTag>(entity);
            }
        }
    }


    private void StartMove(Navigator navigate, Vector3 target)
    {
        navigate.StartPos = navigate.NavigateObj.position;
        navigate.EndPos = new Vector3(target.x, target.y, target.z);
        navigate.Elapsed = 0f;
    }


    private void MoveNavigator(Navigator navigate)
    {
        navigate.NavigateObj.position = Vector3.Lerp(navigate.StartPos, navigate.EndPos, navigate.Elapsed / navigate.NavigateTime);
        navigate.Elapsed += Time.unscaledDeltaTime;
    }
}
