using UnityEngine;
using Core.Character;
using Core.Car;

public class UserCharacterController : MonoBehaviour
{
    [SerializeField] private CharacterBody _characterBody;
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

    private readonly SmoothPressing gasSmoothPressing = new(0.7f, 0.5f);
    private readonly SmoothPressing breakSmoothPressing = new(1f, 5.0f);

    private bool _canMove = true;

    public CarController CarController { get; set; }
    public CharacterBody CharacterBody => _characterBody;

    private void Update()
    {
        CharacterControl();
        CarControl();
    }

    public void SetMoveAbility(bool state)
    {
        _canMove = state;
    }

    private void CarControl()
    {
        if (CarController == null)
        {
            return;
        }

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

        if (Input.GetKeyDown(_setDrivingModeKey))
        {
            CarController.SetDrivingMode();
        }

        if (Input.GetKeyDown(_setReverseModeKey))
        {
            CarController.SetReverseMode();
        }

        if (Input.GetKeyDown(_setParkingModeKey))
        {
            CarController.SetParkingMode();
        }

        if (Input.GetKey(_steerLeftKey))
        {
            CarController.SteerLeft(Time.deltaTime);
        }

        if (Input.GetKey(_steerRightKey))
        {
            CarController.SteerRight(Time.deltaTime);
        }

        if (Input.GetKeyDown(_engineSwitchKey))
        {
            CarController.EngineSwitch();
        }

        if (Input.GetKeyDown(_parkingBreakKey))
        {
            CarController.ParkingBreakSwitch();
        }

        gasSmoothPressing.FullPush =
            breakSmoothPressing.FullPush =
            Input.GetKey(_addPowerKey);

        CarController.GasPedalPress(gasSmoothPressing.Value);
        CarController.BreakPedalPress(breakSmoothPressing.Value);
    }

    private void CharacterControl()
    {
        var forward = _canMove && Input.GetKey(_forwardKey) ?
            Movement.POSITIVE :
            Movement.NONE;

        var back = _canMove && Input.GetKey(_backKey) ?
            Movement.NEGATIVE :
            Movement.NONE;

        var right = _canMove && Input.GetKey(_rightKey) ?
            Movement.POSITIVE :
            Movement.NONE;

        var left = _canMove && Input.GetKey(_leftKey) ?
            Movement.NEGATIVE :
            Movement.NONE;

        var horizontal = (Movement)((int)right + (int)left);
        var vertical = (Movement)((int)forward + (int)back);

        _characterBody.Move(horizontal, vertical);

        _characterBody.IsRunning = _canMove && Input.GetKey(_runKey);
        _characterBody.IsJumping = _canMove && Input.GetKey(_jumpKey);

        var rotationDelta = new Vector3(
            _canMove ? Input.GetAxis("Mouse X") * _mouseSensitivity : 0,
            _canMove ? Input.GetAxis("Mouse Y") * _mouseSensitivity : 0
        );

        _characterBody.UpdateView(rotationDelta);

        if (Input.GetKeyDown(_leaveKey))
        {
            _characterBody.IsSitting = false;
        }
    }
}
