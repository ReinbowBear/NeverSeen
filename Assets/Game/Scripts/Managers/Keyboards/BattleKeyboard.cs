using UnityEngine;

public class BattleKeyboard : MonoBehaviour
{
    public static GameInput gameInput;
    private Entity character;

    void Awake()
    {
        gameInput = new GameInput();
    }

    private void GetCharacter(MyEvent.OnEntityInit newEvent)
    {
        if (newEvent.entity.baseStats.isPlayer == true)
        {
            character = newEvent.entity;
        }
    }

    private void KeyboardAbility(byte index)
    {
        character.ChoseAbility(index);
    }


    void OnEnable()
    {
        gameInput.Enable();

        gameInput.Player.Slot_0.started += context => KeyboardAbility(0);
        gameInput.Player.Slot_1.started += context => KeyboardAbility(1);
        gameInput.Player.Slot_2.started += context => KeyboardAbility(2);
        gameInput.Player.Slot_3.started += context => KeyboardAbility(3);
        gameInput.Player.Slot_4.started += context => KeyboardAbility(4);
        gameInput.Player.Slot_5.started += context => KeyboardAbility(5);
        gameInput.Player.Slot_6.started += context => KeyboardAbility(6);
        gameInput.Player.Slot_7.started += context => KeyboardAbility(7);

        EventBus.Add<MyEvent.OnEntityInit>(GetCharacter);
    }

    void OnDisable()
    {
        gameInput.Player.Slot_0.started -= context => KeyboardAbility(0);
        gameInput.Player.Slot_1.started -= context => KeyboardAbility(1);
        gameInput.Player.Slot_2.started -= context => KeyboardAbility(2);
        gameInput.Player.Slot_3.started -= context => KeyboardAbility(3);
        gameInput.Player.Slot_4.started -= context => KeyboardAbility(4);
        gameInput.Player.Slot_5.started -= context => KeyboardAbility(5);
        gameInput.Player.Slot_6.started -= context => KeyboardAbility(6);
        gameInput.Player.Slot_7.started -= context => KeyboardAbility(7);

        EventBus.Remove<MyEvent.OnEntityInit>(GetCharacter);

        gameInput.Disable();
    }
}
