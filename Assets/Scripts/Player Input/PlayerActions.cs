// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player Input/PlayerActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Player Input"",
            ""id"": ""2fa7c70f-06e6-4749-9aad-6144b8a0ea88"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""43dd1be6-9018-4c44-ba1b-0164c2162e1f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)""
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""366e5ba4-ec9f-4f90-9965-21ab8d875849"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""46906412-4138-41a5-acca-de4af49e2120"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""WallRun"",
                    ""type"": ""Button"",
                    ""id"": ""5b8452f1-90ce-4cb6-8e6f-b75eefcefb52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)""
                },
                {
                    ""name"": ""Crouch/Slide"",
                    ""type"": ""Button"",
                    ""id"": ""3ba404bb-3d73-4668-995f-3a359ab35913"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""c96a44e3-b405-49cc-9f6a-d134e1828980"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6537b1d5-238b-435f-8eec-0f0b20b2ac41"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""47cd7228-abb9-4f73-b491-e5ed4f23fa5f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bb92f57b-72f0-46b9-bef2-f4cd57117932"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""77e87f78-b6e2-4340-83d9-1af091e2df6e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7fe05edd-5580-48cc-8454-e67d38d2223e"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff6cbb56-66ed-4efc-9146-5c6690288b7c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""506378e8-2d80-4a66-8e70-6430d17b7bd6"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WallRun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f39d4772-8465-4aac-970a-cec4b8017fe0"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch/Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Input
        m_PlayerInput = asset.FindActionMap("Player Input", throwIfNotFound: true);
        m_PlayerInput_Move = m_PlayerInput.FindAction("Move", throwIfNotFound: true);
        m_PlayerInput_Sprint = m_PlayerInput.FindAction("Sprint", throwIfNotFound: true);
        m_PlayerInput_Jump = m_PlayerInput.FindAction("Jump", throwIfNotFound: true);
        m_PlayerInput_WallRun = m_PlayerInput.FindAction("WallRun", throwIfNotFound: true);
        m_PlayerInput_CrouchSlide = m_PlayerInput.FindAction("Crouch/Slide", throwIfNotFound: true);
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

    // Player Input
    private readonly InputActionMap m_PlayerInput;
    private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
    private readonly InputAction m_PlayerInput_Move;
    private readonly InputAction m_PlayerInput_Sprint;
    private readonly InputAction m_PlayerInput_Jump;
    private readonly InputAction m_PlayerInput_WallRun;
    private readonly InputAction m_PlayerInput_CrouchSlide;
    public struct PlayerInputActions
    {
        private @PlayerActions m_Wrapper;
        public PlayerInputActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerInput_Move;
        public InputAction @Sprint => m_Wrapper.m_PlayerInput_Sprint;
        public InputAction @Jump => m_Wrapper.m_PlayerInput_Jump;
        public InputAction @WallRun => m_Wrapper.m_PlayerInput_WallRun;
        public InputAction @CrouchSlide => m_Wrapper.m_PlayerInput_CrouchSlide;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMove;
                @Sprint.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSprint;
                @Jump.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnJump;
                @WallRun.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnWallRun;
                @WallRun.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnWallRun;
                @WallRun.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnWallRun;
                @CrouchSlide.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnCrouchSlide;
                @CrouchSlide.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnCrouchSlide;
                @CrouchSlide.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnCrouchSlide;
            }
            m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @WallRun.started += instance.OnWallRun;
                @WallRun.performed += instance.OnWallRun;
                @WallRun.canceled += instance.OnWallRun;
                @CrouchSlide.started += instance.OnCrouchSlide;
                @CrouchSlide.performed += instance.OnCrouchSlide;
                @CrouchSlide.canceled += instance.OnCrouchSlide;
            }
        }
    }
    public PlayerInputActions @PlayerInput => new PlayerInputActions(this);
    public interface IPlayerInputActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnWallRun(InputAction.CallbackContext context);
        void OnCrouchSlide(InputAction.CallbackContext context);
    }
}
