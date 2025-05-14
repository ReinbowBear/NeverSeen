using UnityEngine;
using UnityEngine.InputSystem;

public class BattleKeyboard : MonoBehaviour
{
    public static GameInput gameInput;
    private Move move;

    private Camera cam;
    private InputAction moveInput;
    private Vector2 direction;

    void Awake()
    {
        gameInput = new GameInput();
        moveInput = gameInput.Player.WASD;
        cam = Camera.main;

        move = Character.instance.gameObject.GetComponent<Move>();
    }

    void FixedUpdate()
    {
        direction = moveInput.ReadValue<Vector2>();

        if (direction != Vector2.zero)
        {
            move.MoveTo(direction);
        }

    }


    public void KeyboardAbility(int index)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 20, LayerMask.GetMask("Ground")))
        {
            Character.instance.TryAttack(index, hit.point);
        }
    }


    void OnEnable()
    {
        gameInput.Enable();

        gameInput.Player.Mouse_0.started += context => KeyboardAbility(0);
        gameInput.Player.Mouse_1.started += context => KeyboardAbility(1);
        gameInput.Player.Shift.started += context => KeyboardAbility(2);
    }

    void OnDisable()
    {
        gameInput.Player.Mouse_0.started -= context => KeyboardAbility(0);
        gameInput.Player.Mouse_1.started -= context => KeyboardAbility(1);
        gameInput.Player.Shift.started -= context => KeyboardAbility(2);

        gameInput.Disable();
    }
}
