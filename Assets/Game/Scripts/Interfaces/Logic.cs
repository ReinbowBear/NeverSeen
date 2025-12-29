using UnityEngine;

public interface IToggle
{
    void Toggle();
}

public interface IViewMode
{
    LayerMask GetRayLayer();
    void LeftClick(RaycastHit hit);
    void RightClick();
}
