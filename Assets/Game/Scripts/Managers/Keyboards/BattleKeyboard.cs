using UnityEngine;
using UnityEngine.InputSystem;

public class BattleKeyboard : MonoBehaviour
{
    public static GameInput gameInput;
    [SerializeField] private Character character;

    private InputAction moveInput;
    private Vector2 direction;

    void Awake()
    {
        gameInput = new GameInput();
        moveInput = gameInput.Player.WASD;
    }

    void FixedUpdate()
    {
        direction = moveInput.ReadValue<Vector2>();

        if (direction != Vector2.zero)
        {
            character.move.MoveTo(direction);
        }

    }


    public void KeyboardAbility(int index)
    {
        character.DoAttack(index);
    }


    void OnEnable()
    {
        gameInput.Enable();

        gameInput.Player.Mouse_0.started += context => KeyboardAbility(0);
        gameInput.Player.Mouse_1.started += context => KeyboardAbility(1);
        gameInput.Player.Shift.started += context => KeyboardAbility(2);
        gameInput.Player.Space.started += context => KeyboardAbility(3);
    }

    void OnDisable()
    {
        gameInput.Player.Mouse_0.started -= context => KeyboardAbility(0);
        gameInput.Player.Mouse_1.started -= context => KeyboardAbility(1);
        gameInput.Player.Shift.started -= context => KeyboardAbility(2);
        gameInput.Player.Space.started -= context => KeyboardAbility(3);

        gameInput.Disable();
    }
}
