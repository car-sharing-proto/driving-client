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
    [SerializeField] private float _mouseSensitivity = 10;
    protected override void GetInput()
    {
        IsRuning = Input.GetKey(_runKey);
        IsJumping = Input.GetKey(_jumpKey);

        var forward = Input.GetKey(_forwardKey) ? Movement.POSITIVE : Movement.NONE;
        var back = Input.GetKey(_backKey) ? Movement.NEGATIVE : Movement.NONE;
        var right = Input.GetKey(_rightKey) ? Movement.POSITIVE : Movement.NONE;
        var left = Input.GetKey(_leftKey) ? Movement.NEGATIVE : Movement.NONE;

        HorizontalMovement = (Movement)((int)right + (int)left);
        VerticalMovement = (Movement)((int)forward + (int)back);

        var rotationDelta = RotationDelta;

        rotationDelta.x = Input.GetAxis("Mouse X") * _mouseSensitivity;
        rotationDelta.y = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        RotationDelta = rotationDelta;
    }
}
