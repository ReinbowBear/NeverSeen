using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ProxyCamera : MonoBehaviour, IEventListener
{
    private CameraDragHandler dragHandler = new();
    private CameraRotateHandler rotateHandler = new();
    private CameraZoomHandler zoomHandler = new();

    public void SetEvents(EventWorld eventWorld)
    {
        eventWorld.AddListener<CallbackContext>(this, dragHandler.OnDragStarted, Events.GamePlayInput.RightClick);

        eventWorld.AddListener<CallbackContext>(this, rotateHandler.OnRotateLeft, Events.GamePlayInput.Q);
        eventWorld.AddListener<CallbackContext>(this, rotateHandler.OnRotateRight, Events.GamePlayInput.E);

        eventWorld.AddListener<CallbackContext>(this, zoomHandler.OnZoom, Events.GamePlayInput.Scroll);
    }
}
