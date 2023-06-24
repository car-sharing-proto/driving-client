using UnityEngine;
using Core.Character;
using Core.Car;

public class UserCharacterController : MonoBehaviour
{
    [SerializeField] private CharacterBody _characterBody;
    [SerializeField] private KeyCode _forwardKey = KeyCode.W;
    [SerializeField] private KeyCode _backKey = KeyCode.S;
    [SerializeField] private KeyCode _rightKey = KeyCode.D;
    [SerializeField] private KeyCode _leftKey = KeyCode.A;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _runKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode _leaveKey = KeyCode.LeftShift;
    [SerializeField] private float _mouseSensitivity = 10;

    private bool _canMove = true;

    public CarController CarController { get; set; }
    public CharacterBody CharacterBody => _characterBody;

    private void Update()
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

    public void SetMoveAbility(bool state)
    {
        _canMove = state;
    }
}
