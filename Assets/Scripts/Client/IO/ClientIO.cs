using Core.GameManagment;
using System;
using UnityEngine;

[Serializable]
public class ClientIO :
    Core.Car.IControls,
    Core.Player.IControls
{
    [Header("Character controls")]
    [SerializeField] private KeyCode _forwardKey = KeyCode.W;
    [SerializeField] private KeyCode _backKey = KeyCode.S;
    [SerializeField] private KeyCode _rightKey = KeyCode.D;
    [SerializeField] private KeyCode _leftKey = KeyCode.A;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _runKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode _leaveKey = KeyCode.LeftShift;
    [SerializeField] private float _mouseSensitivity = 2;

    [Header("Car controls")]
    [SerializeField] private KeyCode _gasKey = KeyCode.W;
    [SerializeField] private KeyCode _breakKey = KeyCode.Q;
    [SerializeField] private KeyCode _setDrivingModeKey = KeyCode.O;
    [SerializeField] private KeyCode _setParkingModeKey = KeyCode.P;
    [SerializeField] private KeyCode _setReverseModeKey = KeyCode.U;
    [SerializeField] private KeyCode _setNeutralModeKey = KeyCode.I;
    [SerializeField] private KeyCode _steerRightKey = KeyCode.D;
    [SerializeField] private KeyCode _steerLeftKey = KeyCode.A;
    [SerializeField] private KeyCode _engineSwitchKey = KeyCode.T;
    [SerializeField] private KeyCode _parkingBreakKey = KeyCode.R;
    [SerializeField] private KeyCode _addPowerKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode _leftTurnKey = KeyCode.Comma;
    [SerializeField] private KeyCode _rightTurnKey = KeyCode.Period;
    [SerializeField] private KeyCode _emergencyKey = KeyCode.F;
    [SerializeField] private KeyCode _headLightKey = KeyCode.Tab;

    [Header("Other controls")]
    [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;
    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    [SerializeField] private KeyCode _switchViewKey = KeyCode.V;

    private readonly SmoothPressing gasSmoothPressing = new(0.5f, 0.5f, 0.5f);
    private readonly SmoothPressing breakSmoothPressing = new(1f, 5.0f, 0.6f);

    private GameState _gameState;
    private InteractiveRaycast _interactiveRaycast;
    private ViewSwitcher _viewSwitcher;

    // Car controls.
    public float Gas { get; private set; }
    public float Break { get; private set; }
    public float SteerDelta { get; private set; }
    public bool SetDrivingMode { get; private set; }
    public bool SetParkingMode { get; private set; }
    public bool SetReverseMode { get; private set; }
    public bool SetNeutralMode { get; private set; }
    public bool EngineSwitch { get; private set; }
    public bool ParkingBreakSwitch { get; private set; }
    public bool EmergencySwitch { get; private set; }
    public bool LeftTurnSwitch { get; private set; }
    public bool RightTurnSwitch { get; private set; }
    public bool HeadLightSwitch { get; private set; }

    // Character controls.
    public float RotationDeltaX { get; private set; }
    public float RotationDeltaY { get; private set; }
    public bool MoveForward { get; private set; }
    public bool MoveBack { get; private set; }
    public bool MoveRight { get; private set; }
    public bool MoveLeft { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsJumping { get; private set; }
    public bool Leave { get; private set; }

    public void Initialize(GameState gameState, 
        InteractiveRaycast interactiveRaycast, 
        ViewSwitcher viewSwitcher)
    {
        this._gameState = gameState;
        this._interactiveRaycast = interactiveRaycast;
        this._viewSwitcher = viewSwitcher;

        MouseController.SetVisibility(false);
    }

    public void Update()
    {
        HandleViewSwitching();
        HandlePauseSwitch();
        HandlePlayerInput();
        HandleCarInput();
        HandleInteract();
    }

    private void HandlePlayerInput()
    {
        RotationDeltaX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        RotationDeltaY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        MoveForward = Input.GetKey(_forwardKey);
        MoveBack = Input.GetKey(_backKey);
        MoveRight = Input.GetKey(_rightKey);
        MoveLeft = Input.GetKey(_leftKey);
        IsRunning = Input.GetKey(_runKey);
        IsJumping = Input.GetKey(_jumpKey);
        Leave = Input.GetKeyDown(_leaveKey);
    }

    private void HandleCarInput()
    {
        if (Input.GetKey(_gasKey))
        {
            gasSmoothPressing.Press();
        }
        else
        {
            gasSmoothPressing.Release();
        }

        if (Input.GetKey(_breakKey))
        {
            breakSmoothPressing.Press();
        }
        else
        {
            breakSmoothPressing.Release();
        }

        gasSmoothPressing.FullPush =
            breakSmoothPressing.FullPush =
            Input.GetKey(_addPowerKey);

        Gas = gasSmoothPressing.Value;
        Break = breakSmoothPressing.Value;

        var rightSteering = Input.GetKey(_steerRightKey) ?
            Time.deltaTime : 0.0f;
        var leftSteering = Input.GetKey(_steerLeftKey) ?
            Time.deltaTime : 0.0f;
        SteerDelta = rightSteering - leftSteering;

        SetDrivingMode = Input.GetKeyDown(_setDrivingModeKey);
        SetReverseMode = Input.GetKeyDown(_setReverseModeKey);
        SetParkingMode = Input.GetKeyDown(_setParkingModeKey);
        SetNeutralMode = Input.GetKeyDown(_setNeutralModeKey);
        LeftTurnSwitch = Input.GetKeyDown(_leftTurnKey);
        RightTurnSwitch = Input.GetKeyDown(_rightTurnKey);
        EmergencySwitch = Input.GetKeyDown(_emergencyKey);
        HeadLightSwitch = Input.GetKeyDown(_headLightKey);
        EngineSwitch = Input.GetKeyDown(_engineSwitchKey);
        ParkingBreakSwitch = Input.GetKeyDown(_parkingBreakKey);
    }

    private void HandlePauseSwitch()
    {
        if (Input.GetKeyDown(_pauseKey))
        {
            _gameState.SwitchPauseState();
        }
    }
    private void HandleInteract()
    {
        if (Input.GetKeyDown(_interactKey) || Input.GetMouseButtonDown(0))
        {
            _interactiveRaycast.TryInteract();
        }
    }

    private void HandleViewSwitching()
    {
        if (Input.GetKeyDown(_switchViewKey))
        {
            _viewSwitcher.Switch();
        }
    }
}
