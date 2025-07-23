using UnityEngine;
using UnityEngine.InputSystem;

public class MapKeyboard : MonoBehaviour
{
    public static GameInput gameInput;

    void Awake()
    {
        gameInput = new GameInput();
    }


    private void ExitToMenu(InputAction.CallbackContext context)
    {
        Scene.Load(0);
    }


    void OnEnable()
    {
        gameInput.Enable();

        gameInput.Menu.Esc.started += ExitToMenu;
    }

    void OnDisable()
    {
        gameInput.Menu.Esc.started -= ExitToMenu;

        gameInput.Disable();
    }
}
