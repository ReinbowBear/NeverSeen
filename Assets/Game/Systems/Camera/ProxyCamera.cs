using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ProxyCamera : MonoBehaviour, IEventListener
{
    private CameraDragHandler dragHandler = new();
    private CameraRotateHandler rotateHandler = new();
    private CameraZoomHandler zoomHandler = new();

    public void SetEvents(EventWorld eventWorld)
    {
        eventWorld.AddListener<CallbackContext>(dragHandler.OnDragStarted, Events.GamePlayInput.RightClick);

        eventWorld.AddListener<CallbackContext>(rotateHandler.OnRotateLeft, Events.GamePlayInput.Q);
        eventWorld.AddListener<CallbackContext>(rotateHandler.OnRotateRight, Events.GamePlayInput.E);

        eventWorld.AddListener<CallbackContext>(zoomHandler.OnZoom, Events.GamePlayInput.Scroll);
    }
}
