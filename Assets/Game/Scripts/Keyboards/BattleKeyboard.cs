using UnityEngine;
using UnityEngine.InputSystem;

public class BattleKeyboard : MonoBehaviour
{
    public static GameInput gameInput;

    private Camera cam;
    private Vector2 direction;

    void Awake()
    {
        gameInput = new GameInput();
        cam = Camera.main;
    }


    void OnEnable()
    {
        gameInput.Enable();
    }

    void OnDisable()
    {
        gameInput.Disable();
    }
}
