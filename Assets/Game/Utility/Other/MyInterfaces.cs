using System.Threading.Tasks;

#region global
public interface IAsyncInitializable
{
    Task AsyncInit();
}
#endregion

#region Entity
public interface ICondition
{
    bool IsConditionMet();
}

public interface IBuildingEffect
{
    void Activate();
    void Deactivate();
}
#endregion

#region Utility
public interface IInspectable
{
    string GetDisplayData();
}
#endregion
