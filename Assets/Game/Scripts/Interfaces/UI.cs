
public interface IActivatable
{
    void Activate();
    void Deactivate();
}

public interface IPanel
{
    void SetActive(bool isActive);
    void SetNavigation(bool isEnable);
}

public interface IBarView
{
    void DrawBar(bool isActive);
    void ChangeValue(int value, int maxValue);
}
