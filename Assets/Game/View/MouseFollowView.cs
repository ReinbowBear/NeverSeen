using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MouseFollowView : MonoBehaviour
{
    public UnityEvent OnMoveToTarget = new();
    public UnityEvent OnStopFollowing = new();

    private GameObject objectToMove;
    private LayerMask layer;
    private InputAction action;

    private Vector3 originalCords;
    private Camera cam;

    public void Init(GameObject objectToMove, LayerMask layer, InputAction action)
    {
        this.objectToMove = objectToMove;
        this.layer = layer;
        this.action = action;

        originalCords = objectToMove.transform.position;
        cam = Camera.main;
    }


    public void StartFollow()
    {
        CoroutineManager.Start(DoMove(), this);
    }

    public void StopFollowing()
    {
        CoroutineManager.Stop(DoMove(), this);

        objectToMove.transform.position = originalCords;
        OnStopFollowing.Invoke();
        Destroy(this);
    }

    private IEnumerator DoMove()
    {
        yield return null;
        while (!Mouse.current.leftButton.wasPressedThisFrame) // в идеале передавать на что реакция но пока так
        {
            Ray ray = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, 50, layer) || objectToMove.transform.position == hit.transform.position)
            {
                yield return null;
                continue;
            }

            objectToMove.transform.position = hit.transform.position;
            originalCords = objectToMove.transform.position;
            OnMoveToTarget.Invoke();

            yield return null;
        }

        Destroy(this);
    }

    void OnDestroy()
    {
        StopFollowing();
    }
}
