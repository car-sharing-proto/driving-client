namespace Core.Player
{
    public class PlayerController
    {
        private readonly IControls _controls;

        private PlayerBody _playerBody;

        public bool IsAvailable { get; set; }

        public PlayerBody PlayerBody => _playerBody;

        public PlayerController(IControls controls)
        {
            this._controls = controls;

            IsAvailable = true;
        }

        public void SetPlayerBody(PlayerBody playerBody)
        {
            this._playerBody = playerBody;
        }

        public void Update()
        {
            if(_playerBody == null) return;

            var forward = IsAvailable && _controls.MoveForward ?
                Movement.POSITIVE :
                Movement.NONE;

            var back = IsAvailable && _controls.MoveBack ?
                Movement.NEGATIVE :
                Movement.NONE;

            var right = IsAvailable && _controls.MoveRight ?
                Movement.POSITIVE :
                Movement.NONE;

            var left = IsAvailable && _controls.MoveLeft ?
                Movement.NEGATIVE :
                Movement.NONE;

            var horizontal = (Movement)((int)right + (int)left);
            var vertical = (Movement)((int)forward + (int)back);

            _playerBody.Move(horizontal, vertical);

            _playerBody.IsRunning = IsAvailable && _controls.IsRunning;
            _playerBody.IsJumping = IsAvailable && _controls.IsJumping;

            var rotationDelta = new UnityEngine.Vector3(
                IsAvailable ? _controls.RotationDeltaX : 0.0f,
                IsAvailable ? _controls.RotationDeltaY : 0.0f
            );

            _playerBody.UpdateView(rotationDelta);

            if (IsAvailable && _controls.Leave)
            {
                _playerBody.IsSitting = false;
            }
        }
    }
}
