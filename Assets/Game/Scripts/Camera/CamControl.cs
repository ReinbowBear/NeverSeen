using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamControl : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [Space]
    [SerializeField] private float dragSensitivity;
    [SerializeField] private float dragVelocity;
    [SerializeField] private float maxPosZ;
    [SerializeField] private float maxPosX;
    [Space]
    [SerializeField] private float rotateSensitivity;
    [SerializeField] private float rotateVelocity;
    [Space]
    [SerializeField] private float zoomSensitivity;
    [SerializeField] private float zoomVelocity;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    private Vector3 currentDragVelocity;

    private Coroutine DraggingCoroutine;
    private Coroutine rotateCoroutine;
    private Coroutine zoomCoroutine;

    private float targetZoom;


    void Awake()
    {
        targetZoom = Vector3.Distance(transform.position, cam.transform.position);
    }


    private void DragCam(InputAction.CallbackContext _)
    {
        if (DraggingCoroutine != null)
        {
            StopCoroutine(DoDragging());
            DraggingCoroutine = null;
        }

        DraggingCoroutine = StartCoroutine(DoDragging());
    }

    private IEnumerator DoDragging()
    {
        Vector3 lastPosition = Input.mousePosition;
        while (BattleKeyboard.gameInput.Player.MouseRight.IsPressed())
        {
            Vector3 delta = Input.mousePosition - lastPosition;
            lastPosition = Input.mousePosition;

            delta.x /= Screen.width; // нормализируем значение в заивисимости от ширины экрана, а то приходится выставлять скорость 0.002 и она переменчива от разрешения
            delta.y /= Screen.height;

            Vector3 move = -transform.right * delta.x - transform.forward * delta.y;
            move.y = 0;

            currentDragVelocity = move.normalized * delta.magnitude * dragSensitivity;

            Vector3 newPos = transform.position + currentDragVelocity;
            newPos.x = Mathf.Clamp(newPos.x, -maxPosX, maxPosX);
            newPos.z = Mathf.Clamp(newPos.z, -maxPosZ, maxPosZ);

            transform.position = newPos;
            yield return null;
        }

        while (currentDragVelocity.magnitude > 0.01f) // инерция камеры
        {
            currentDragVelocity = Vector3.Lerp(currentDragVelocity, Vector3.zero, Time.deltaTime * dragVelocity);
            Vector3 newPos = transform.position + currentDragVelocity;

            newPos.x = Mathf.Clamp(newPos.x, -maxPosX, maxPosX);
            newPos.z = Mathf.Clamp(newPos.z, -maxPosZ, maxPosZ);

            transform.position = newPos;

            yield return null;
        }

        currentDragVelocity = Vector3.zero;
        DraggingCoroutine = null;
    }


    private void RotateCamera(InputAction.CallbackContext _)
    {
        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
            rotateCoroutine = null;
        }

        rotateCoroutine = StartCoroutine(DoRotate());
    }

    private IEnumerator DoRotate()
    {
        float currentRotateVelocity = 0;
        float targetAngle;

        while (BattleKeyboard.gameInput.Player.Q.IsPressed() || BattleKeyboard.gameInput.Player.E.IsPressed() || Mathf.Abs(currentRotateVelocity) > 0.01f)
        {
            if (BattleKeyboard.gameInput.Player.Q.IsPressed())
            {
                targetAngle = rotateSensitivity;
            }
            else if (BattleKeyboard.gameInput.Player.E.IsPressed())
            {
                targetAngle = -rotateSensitivity;
            }
            else
            {
                targetAngle = 0f;
            }

            currentRotateVelocity = Mathf.Lerp(currentRotateVelocity, targetAngle, Time.deltaTime * rotateVelocity);
            transform.rotation *= Quaternion.Euler(0f, currentRotateVelocity * Time.deltaTime, 0f);
            yield return null;
        }

        rotateCoroutine = null;
    }


    private void ZoomCam(InputAction.CallbackContext _)
    {
        Vector2 scrollValue = BattleKeyboard.gameInput.Player.Scroll.ReadValue<Vector2>();

        targetZoom -= scrollValue.y * zoomSensitivity;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);

        if (zoomCoroutine == null)
        {
            zoomCoroutine = StartCoroutine(DoZoom());
        }
    }

    private IEnumerator DoZoom()
    {
        while (Mathf.Abs(Vector3.Distance(cam.transform.position, transform.position) - targetZoom) > 0.01f)
        {
            Vector3 direction = (cam.transform.position - transform.position).normalized;

            float currentDistance = Vector3.Distance(cam.transform.position, transform.position);
            float smoothDistance = Mathf.Lerp(currentDistance, targetZoom, Time.deltaTime * zoomVelocity);

            cam.transform.position = transform.position + direction * smoothDistance;
            yield return null;
        }

        cam.transform.position = transform.position + (cam.transform.position - transform.position).normalized * targetZoom;
        zoomCoroutine = null;
    }


    void Start()
    {
        BattleKeyboard.gameInput.Player.MouseRight.started += DragCam;
        BattleKeyboard.gameInput.Player.MouseRight.canceled += DragCam; // функция проверит что клавиша отжата и отпустит камеру

        BattleKeyboard.gameInput.Player.Q.started += RotateCamera;
        BattleKeyboard.gameInput.Player.E.started += RotateCamera;

        BattleKeyboard.gameInput.Player.Scroll.started += ZoomCam;
    }

    void OnDestroy()
    {
        BattleKeyboard.gameInput.Player.MouseRight.started -= DragCam;
        BattleKeyboard.gameInput.Player.MouseRight.canceled -= DragCam;

        BattleKeyboard.gameInput.Player.Q.started -= RotateCamera;
        BattleKeyboard.gameInput.Player.E.started -= RotateCamera;

        BattleKeyboard.gameInput.Player.Scroll.started -= ZoomCam;
    }
}
