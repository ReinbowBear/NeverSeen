
public interface IActivatable
{
    void Activate();
    void Deactivate();
}

public interface IBarView
{
    void DrawBar(bool isActive);
    void ChangeValue(int value, int maxValue);
}
