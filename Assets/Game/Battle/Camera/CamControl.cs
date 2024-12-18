using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamControl : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [Space]
    [SerializeField] private byte maxPosRange;
    [Space]
    [SerializeField] private byte minCamDist;
    [SerializeField] private byte maxCamDist;
    [Space]
    [SerializeField] private float moveSpeed;
    [Space]
    [SerializeField] private float rotationDuration;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private Vector3 angle = new Vector3(0, 90, 0);

    private Coroutine myCoroutine;
    private bool holdButton;
    private float timeElapsed;


    private void MoveCam(InputAction.CallbackContext context)
    {
        holdButton = true;
        StartCoroutine(HoldCam());
    }

    private void StopCam(InputAction.CallbackContext context)
    {
        holdButton = false;
    }

    private IEnumerator HoldCam()
    {
        Vector3 lastPosition = Input.mousePosition;
        while (holdButton)
        {
            Vector3 delta = Input.mousePosition - lastPosition;
            lastPosition = Input.mousePosition;

            float moveX = delta.x * moveSpeed;
            float moveY = delta.y * moveSpeed;

            Vector3 moveDirection = transform.right * (moveX - moveY) + transform.forward * (moveY + moveX);
            Vector3 newPos = transform.position - moveDirection;
            
            newPos.x = Mathf.Clamp(newPos.x, 0, maxPosRange);
            newPos.z = Mathf.Clamp(newPos.z, 0, maxPosRange);

            transform.position = newPos;

            yield return null;
        }
    }


    private void CamZoom(InputAction.CallbackContext context)
    {
        Vector2 scrollValue = MapKeyboard.gameInput.Player.Scroll.ReadValue<Vector2>();
        Vector3 newPos = cam.transform.position + cam.transform.forward * scrollValue.y/60;

        float distance = Vector3.Distance(newPos, transform.position);

        if (minCamDist <= distance && distance <= maxCamDist)
        {
            cam.transform.position = newPos;
        }
    }


    private void RotateCamera(int rotatePos)
    {
        if (rotatePos == 1)
        {
            if (myCoroutine == null)
            {
                myCoroutine = StartCoroutine(Rotate(-angle));
            }
            else
            {
                startRotation = transform.rotation;
                endRotation = endRotation * Quaternion.Euler(-angle);
                timeElapsed = 0f;
            }
        }
        else
        {
            if (myCoroutine == null)
            {
                myCoroutine = StartCoroutine(Rotate(angle));
            }
            else
            {
                startRotation = transform.rotation;
                endRotation = endRotation * Quaternion.Euler(angle);
                timeElapsed = 0f;
            }
        }
    }

    private IEnumerator Rotate(Vector3 targetAngle)
    {
        startRotation = transform.rotation;
        endRotation = startRotation * Quaternion.Euler(targetAngle);
   
        timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed / rotationDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        transform.rotation = endRotation;
        myCoroutine = null;
    }


    void Start() //тут должен был быть OnEnable, но почему то нулевой референс возникает тут и у Pause
    {
        MapKeyboard.gameInput.Player.Mouse_1.started += MoveCam;
        MapKeyboard.gameInput.Player.Mouse_1.canceled += StopCam;

        MapKeyboard.gameInput.Player.Scroll.started += CamZoom;

        MapKeyboard.gameInput.Player.Q.started += context => RotateCamera(0);
        MapKeyboard.gameInput.Player.E.started += context => RotateCamera(1);
    }

    void OnDestroy()
    {
        MapKeyboard.gameInput.Player.Mouse_1.started -= MoveCam;
        MapKeyboard.gameInput.Player.Mouse_1.canceled -= StopCam;

        MapKeyboard.gameInput.Player.Scroll.started -= CamZoom;

        MapKeyboard.gameInput.Player.Q.started -= context => RotateCamera(0);
        MapKeyboard.gameInput.Player.E.started -= context => RotateCamera(1);
    }
}
