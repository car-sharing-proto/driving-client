using UnityEngine;
using Core.Player;

using Movement = Core.Player.PlayerMovement.Movement;

public sealed class UserController : PlayerController
{
    [SerializeField] private KeyCode _forwardKey = KeyCode.W;
    [SerializeField] private KeyCode _backKey = KeyCode.S;
    [SerializeField] private KeyCode _rightKey = KeyCode.D;
    [SerializeField] private KeyCode _leftKey = KeyCode.A;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _runKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode _leaveKey = KeyCode.LeftShift;
    [SerializeField] private float _mouseSensitivity = 10;

    private bool _canMove = true;

    public void SetMoveAbility(bool state)
    {
        _canMove = state;
    }

    protected override void GetInput()
    {
        IsRuning = _canMove && Input.GetKey(_runKey);
        IsJumping = _canMove && Input.GetKey(_jumpKey);

        var forward = _canMove && Input.GetKey(_forwardKey) ? Movement.POSITIVE : Movement.NONE;
        var back = _canMove && Input.GetKey(_backKey) ? Movement.NEGATIVE : Movement.NONE;
        var right = _canMove && Input.GetKey(_rightKey) ? Movement.POSITIVE : Movement.NONE;
        var left = _canMove && Input.GetKey(_leftKey) ? Movement.NEGATIVE : Movement.NONE;

        HorizontalMovement = (Movement)((int)right + (int)left);
        VerticalMovement = (Movement)((int)forward + (int)back);

        var rotationDelta = RotationDelta;

        rotationDelta.x = Input.GetAxis("Mouse X") * _mouseSensitivity;
        rotationDelta.y = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        RotationDelta = _canMove ? rotationDelta : Vector3.zero;

        if (Input.GetKeyDown(_leaveKey))
        {
            IsSitting = false;
        }
    }
}
