//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Game/Static/Inputs/GameInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @GameInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""4f7610fa-d977-48ee-a3b2-e12f169670c2"",
            ""actions"": [
                {
                    ""name"": ""Mouse_0"",
                    ""type"": ""Button"",
                    ""id"": ""32ddf092-c814-449b-a598-faa6f357bfdf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mouse_1"",
                    ""type"": ""Button"",
                    ""id"": ""713377ac-6c6b-4f0a-8753-6bdcda097265"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_0"",
                    ""type"": ""Button"",
                    ""id"": ""0eb0df65-7fec-4145-ad9c-1bad7b314952"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_1"",
                    ""type"": ""Button"",
                    ""id"": ""156f7551-d618-471c-8a48-7386e7043aa2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_2"",
                    ""type"": ""Button"",
                    ""id"": ""1b7631b5-8d8f-4c08-ab70-8e6b8aaac233"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_3"",
                    ""type"": ""Button"",
                    ""id"": ""93defc22-6ebc-4f66-83b6-0ab17de72ba8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_4"",
                    ""type"": ""Button"",
                    ""id"": ""5e7bb85b-1a1c-4b57-95ed-3db7d92c757e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_5"",
                    ""type"": ""Button"",
                    ""id"": ""6af0f3b9-6f09-4e07-98f0-d483d2062cda"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_6"",
                    ""type"": ""Button"",
                    ""id"": ""a808d4d0-80de-40b2-b4c5-d6f3355c983e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_7"",
                    ""type"": ""Button"",
                    ""id"": ""c25e28dd-c766-409f-bab1-12579da92fd8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Esc"",
                    ""type"": ""Button"",
                    ""id"": ""7e1b80c9-9e8f-43cf-8820-6f9551b20868"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""05f6913d-c316-48b2-a6bb-e225f14c7960"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Mouse_0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""819dac51-ad7b-4498-a601-b944e3a08ebb"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Slot_0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4fc215c-410f-4090-a1ef-04163c6bc1b4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Slot_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""52c20905-81d2-4036-bb23-162d8f09b005"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Mouse_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5068740-7e53-42bc-958f-f00d1cff5218"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Esc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3f39a01-3f8a-4ae1-9b4d-138dada3b7d1"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Slot_2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75eff98f-2e0b-4850-a2f7-077fe252346d"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Slot_3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25f1147e-e277-491e-88aa-ce2f5cc5a5ff"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Slot_4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8443f672-5d14-48dd-a24c-6a1fcfcd2d0d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Slot_5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5c940c3-ff05-4d03-b389-04700c64e53a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Slot_6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""073e736a-a648-4f4c-ae55-503cd097f5a5"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Slot_7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""4f03bb9b-1a89-4e86-91c9-d408d8ea58d6"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Button"",
                    ""id"": ""e952c79d-5d34-431f-992b-672903dceaae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""e4fc27dc-0f96-4b11-b949-2e19d60f4254"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Esc"",
                    ""type"": ""Button"",
                    ""id"": ""725d1de0-dfb1-4edc-ba82-a655f54a30a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""ff527021-f211-4c02-933e-5976594c46ed"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""563fbfdd-0f09-408d-aa75-8642c4f08ef0"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""eb480147-c587-4a33-85ed-eb0ab9942c43"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9e92bb26-7e3b-4ec4-b06b-3c8f8e498ddc"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82627dcc-3b13-4ba9-841d-e4b746d6553e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Esc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XR"",
            ""bindingGroup"": ""XR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Mouse_0 = m_Player.FindAction("Mouse_0", throwIfNotFound: true);
        m_Player_Mouse_1 = m_Player.FindAction("Mouse_1", throwIfNotFound: true);
        m_Player_Slot_0 = m_Player.FindAction("Slot_0", throwIfNotFound: true);
        m_Player_Slot_1 = m_Player.FindAction("Slot_1", throwIfNotFound: true);
        m_Player_Slot_2 = m_Player.FindAction("Slot_2", throwIfNotFound: true);
        m_Player_Slot_3 = m_Player.FindAction("Slot_3", throwIfNotFound: true);
        m_Player_Slot_4 = m_Player.FindAction("Slot_4", throwIfNotFound: true);
        m_Player_Slot_5 = m_Player.FindAction("Slot_5", throwIfNotFound: true);
        m_Player_Slot_6 = m_Player.FindAction("Slot_6", throwIfNotFound: true);
        m_Player_Slot_7 = m_Player.FindAction("Slot_7", throwIfNotFound: true);
        m_Player_Esc = m_Player.FindAction("Esc", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Navigate = m_Menu.FindAction("Navigate", throwIfNotFound: true);
        m_Menu_Submit = m_Menu.FindAction("Submit", throwIfNotFound: true);
        m_Menu_Esc = m_Menu.FindAction("Esc", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Mouse_0;
    private readonly InputAction m_Player_Mouse_1;
    private readonly InputAction m_Player_Slot_0;
    private readonly InputAction m_Player_Slot_1;
    private readonly InputAction m_Player_Slot_2;
    private readonly InputAction m_Player_Slot_3;
    private readonly InputAction m_Player_Slot_4;
    private readonly InputAction m_Player_Slot_5;
    private readonly InputAction m_Player_Slot_6;
    private readonly InputAction m_Player_Slot_7;
    private readonly InputAction m_Player_Esc;
    public struct PlayerActions
    {
        private @GameInput m_Wrapper;
        public PlayerActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Mouse_0 => m_Wrapper.m_Player_Mouse_0;
        public InputAction @Mouse_1 => m_Wrapper.m_Player_Mouse_1;
        public InputAction @Slot_0 => m_Wrapper.m_Player_Slot_0;
        public InputAction @Slot_1 => m_Wrapper.m_Player_Slot_1;
        public InputAction @Slot_2 => m_Wrapper.m_Player_Slot_2;
        public InputAction @Slot_3 => m_Wrapper.m_Player_Slot_3;
        public InputAction @Slot_4 => m_Wrapper.m_Player_Slot_4;
        public InputAction @Slot_5 => m_Wrapper.m_Player_Slot_5;
        public InputAction @Slot_6 => m_Wrapper.m_Player_Slot_6;
        public InputAction @Slot_7 => m_Wrapper.m_Player_Slot_7;
        public InputAction @Esc => m_Wrapper.m_Player_Esc;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Mouse_0.started += instance.OnMouse_0;
            @Mouse_0.performed += instance.OnMouse_0;
            @Mouse_0.canceled += instance.OnMouse_0;
            @Mouse_1.started += instance.OnMouse_1;
            @Mouse_1.performed += instance.OnMouse_1;
            @Mouse_1.canceled += instance.OnMouse_1;
            @Slot_0.started += instance.OnSlot_0;
            @Slot_0.performed += instance.OnSlot_0;
            @Slot_0.canceled += instance.OnSlot_0;
            @Slot_1.started += instance.OnSlot_1;
            @Slot_1.performed += instance.OnSlot_1;
            @Slot_1.canceled += instance.OnSlot_1;
            @Slot_2.started += instance.OnSlot_2;
            @Slot_2.performed += instance.OnSlot_2;
            @Slot_2.canceled += instance.OnSlot_2;
            @Slot_3.started += instance.OnSlot_3;
            @Slot_3.performed += instance.OnSlot_3;
            @Slot_3.canceled += instance.OnSlot_3;
            @Slot_4.started += instance.OnSlot_4;
            @Slot_4.performed += instance.OnSlot_4;
            @Slot_4.canceled += instance.OnSlot_4;
            @Slot_5.started += instance.OnSlot_5;
            @Slot_5.performed += instance.OnSlot_5;
            @Slot_5.canceled += instance.OnSlot_5;
            @Slot_6.started += instance.OnSlot_6;
            @Slot_6.performed += instance.OnSlot_6;
            @Slot_6.canceled += instance.OnSlot_6;
            @Slot_7.started += instance.OnSlot_7;
            @Slot_7.performed += instance.OnSlot_7;
            @Slot_7.canceled += instance.OnSlot_7;
            @Esc.started += instance.OnEsc;
            @Esc.performed += instance.OnEsc;
            @Esc.canceled += instance.OnEsc;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Mouse_0.started -= instance.OnMouse_0;
            @Mouse_0.performed -= instance.OnMouse_0;
            @Mouse_0.canceled -= instance.OnMouse_0;
            @Mouse_1.started -= instance.OnMouse_1;
            @Mouse_1.performed -= instance.OnMouse_1;
            @Mouse_1.canceled -= instance.OnMouse_1;
            @Slot_0.started -= instance.OnSlot_0;
            @Slot_0.performed -= instance.OnSlot_0;
            @Slot_0.canceled -= instance.OnSlot_0;
            @Slot_1.started -= instance.OnSlot_1;
            @Slot_1.performed -= instance.OnSlot_1;
            @Slot_1.canceled -= instance.OnSlot_1;
            @Slot_2.started -= instance.OnSlot_2;
            @Slot_2.performed -= instance.OnSlot_2;
            @Slot_2.canceled -= instance.OnSlot_2;
            @Slot_3.started -= instance.OnSlot_3;
            @Slot_3.performed -= instance.OnSlot_3;
            @Slot_3.canceled -= instance.OnSlot_3;
            @Slot_4.started -= instance.OnSlot_4;
            @Slot_4.performed -= instance.OnSlot_4;
            @Slot_4.canceled -= instance.OnSlot_4;
            @Slot_5.started -= instance.OnSlot_5;
            @Slot_5.performed -= instance.OnSlot_5;
            @Slot_5.canceled -= instance.OnSlot_5;
            @Slot_6.started -= instance.OnSlot_6;
            @Slot_6.performed -= instance.OnSlot_6;
            @Slot_6.canceled -= instance.OnSlot_6;
            @Slot_7.started -= instance.OnSlot_7;
            @Slot_7.performed -= instance.OnSlot_7;
            @Slot_7.canceled -= instance.OnSlot_7;
            @Esc.started -= instance.OnEsc;
            @Esc.performed -= instance.OnEsc;
            @Esc.canceled -= instance.OnEsc;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private List<IMenuActions> m_MenuActionsCallbackInterfaces = new List<IMenuActions>();
    private readonly InputAction m_Menu_Navigate;
    private readonly InputAction m_Menu_Submit;
    private readonly InputAction m_Menu_Esc;
    public struct MenuActions
    {
        private @GameInput m_Wrapper;
        public MenuActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_Menu_Navigate;
        public InputAction @Submit => m_Wrapper.m_Menu_Submit;
        public InputAction @Esc => m_Wrapper.m_Menu_Esc;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void AddCallbacks(IMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_MenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MenuActionsCallbackInterfaces.Add(instance);
            @Navigate.started += instance.OnNavigate;
            @Navigate.performed += instance.OnNavigate;
            @Navigate.canceled += instance.OnNavigate;
            @Submit.started += instance.OnSubmit;
            @Submit.performed += instance.OnSubmit;
            @Submit.canceled += instance.OnSubmit;
            @Esc.started += instance.OnEsc;
            @Esc.performed += instance.OnEsc;
            @Esc.canceled += instance.OnEsc;
        }

        private void UnregisterCallbacks(IMenuActions instance)
        {
            @Navigate.started -= instance.OnNavigate;
            @Navigate.performed -= instance.OnNavigate;
            @Navigate.canceled -= instance.OnNavigate;
            @Submit.started -= instance.OnSubmit;
            @Submit.performed -= instance.OnSubmit;
            @Submit.canceled -= instance.OnSubmit;
            @Esc.started -= instance.OnEsc;
            @Esc.performed -= instance.OnEsc;
            @Esc.canceled -= instance.OnEsc;
        }

        public void RemoveCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_MenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    private int m_XRSchemeIndex = -1;
    public InputControlScheme XRScheme
    {
        get
        {
            if (m_XRSchemeIndex == -1) m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
            return asset.controlSchemes[m_XRSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMouse_0(InputAction.CallbackContext context);
        void OnMouse_1(InputAction.CallbackContext context);
        void OnSlot_0(InputAction.CallbackContext context);
        void OnSlot_1(InputAction.CallbackContext context);
        void OnSlot_2(InputAction.CallbackContext context);
        void OnSlot_3(InputAction.CallbackContext context);
        void OnSlot_4(InputAction.CallbackContext context);
        void OnSlot_5(InputAction.CallbackContext context);
        void OnSlot_6(InputAction.CallbackContext context);
        void OnSlot_7(InputAction.CallbackContext context);
        void OnEsc(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnEsc(InputAction.CallbackContext context);
    }
}