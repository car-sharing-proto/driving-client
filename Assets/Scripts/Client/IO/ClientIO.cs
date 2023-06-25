using Core.Car;
using Core.Player;
using System.Collections.Generic;
using UnityEngine;

public class ClientIO : MonoBehaviour, 
    Core.Car.IControls, 
    Core.Player.IControls 
{
    [SerializeField] private GameObject _cursor;
    [SerializeField] private PlayerBody _playerBody;

    [Header("Character controls")]
    [SerializeField] private KeyCode _forwardKey = KeyCode.W;
    [SerializeField] private KeyCode _backKey = KeyCode.S;
    [SerializeField] private KeyCode _rightKey = KeyCode.D;
    [SerializeField] private KeyCode _leftKey = KeyCode.A;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _runKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode _leaveKey = KeyCode.LeftShift;
    [SerializeField] private float _mouseSensitivity = 10;
    [Header("Car controls")]
    [SerializeField] private KeyCode _gasKey = KeyCode.W;
    [SerializeField] private KeyCode _breakKey = KeyCode.Q;
    [SerializeField] private KeyCode _setDrivingModeKey = KeyCode.O;
    [SerializeField] private KeyCode _setParkingModeKey = KeyCode.P;
    [SerializeField] private KeyCode _setReverseModeKey = KeyCode.I;
    [SerializeField] private KeyCode _steerRightKey = KeyCode.D;
    [SerializeField] private KeyCode _steerLeftKey = KeyCode.A;
    [SerializeField] private KeyCode _engineSwitchKey = KeyCode.T;
    [SerializeField] private KeyCode _parkingBreakKey = KeyCode.R;
    [SerializeField] private KeyCode _addPowerKey = KeyCode.LeftControl;
    [Header("Other controls")]
    [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;

    private readonly SmoothPressing gasSmoothPressing = new(0.7f, 0.5f);
    private readonly SmoothPressing breakSmoothPressing = new(1f, 5.0f);

    private List<ViewProbeHolder> _viewProbeHolders;
    private User _userController;
    private bool _isPause = false;

    public float Gas { get; private set; }
    public float Break { get; private set; }
    public float SteerDelta { get; private set; }
    public bool SetDrivingMode { get; private set; }
    public bool SetParkingMode { get; private set; }
    public bool SetReverseMode { get; private set; }
    public bool EngineSwitch { get; private set; }
    public bool ParkingBreakSwitch { get; private set; }
    public float RotationDeltaX { get; private set; }
    public float RotationDeltaY { get; private set; }
    public bool MoveForward { get; private set; }
    public bool MoveBack { get; private set; }
    public bool MoveRight { get; private set; }
    public bool MoveLeft { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsJumping { get; private set; }
    public bool Leave { get; private set; }

    private void Awake ()
    {
        _userController = new()
        {
            PlayerController = new(_playerBody)
        };

        _viewProbeHolders = new()
        {
            new ViewProbe<IFunctional>(
                _playerBody.HeadTransform,
                3f, probe => probe.Interact(),
                probe => probe.IsInteractable),
            new ViewProbe<SeatController>(
                _playerBody.HeadTransform,
                2f, probe => probe.Take(_userController),
                probe => probe.IsInteractable(_userController)),
        };

        MouseController.SetVisibility(false);
        _userController.SetMoveAbility(true);
    }

    private void Update()
    {
        CheckViewProbes();
        CheckPauseSwitch();
        UpdateInput();

        _userController.CarControl(this);
        _userController.PlayerControl(this);
    }

    private void UpdateInput()
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
        RotationDeltaX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        RotationDeltaY = Input.GetAxis("Mouse Y") * _mouseSensitivity;
        EngineSwitch = Input.GetKeyDown(_engineSwitchKey);
        ParkingBreakSwitch = Input.GetKeyDown(_parkingBreakKey);
        MoveForward = Input.GetKey(_forwardKey);
        MoveBack = Input.GetKey(_backKey);
        MoveRight = Input.GetKey(_rightKey);
        MoveLeft = Input.GetKey(_leftKey);
        IsRunning = Input.GetKey(_runKey);
        IsJumping = Input.GetKey(_jumpKey);
        Leave = Input.GetKeyDown(_leaveKey);
    }

    private void CheckViewProbes()
    {
        _cursor.SetActive(false);

        foreach (var holder in _viewProbeHolders)
        {
            var mode = Input.GetKeyDown(KeyCode.E) ?
                ViewProbeHolder.QueryMode.INTERACT :
                ViewProbeHolder.QueryMode.CHECK;

            if (holder.CheckCondition(mode))
            {
                _cursor.SetActive(true);
            }
        }
    }

    private void CheckPauseSwitch()
    {
        if (Input.GetKeyDown(_pauseKey))
        {
            _isPause = !_isPause;

            MouseController.SetVisibility(_isPause);
            _userController.SetMoveAbility(!_isPause);
        }
    }
}
