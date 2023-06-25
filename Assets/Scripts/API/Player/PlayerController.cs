namespace Core.Player
{
    public class PlayerController
    {
        private readonly PlayerBody _playerBody;

        public bool IsAvailable { get; set; }
        public PlayerBody PlayerBody => _playerBody;

        public PlayerController(PlayerBody playerBody)
        {
            this._playerBody = playerBody;
        }

        public void Update(IControls controls)
        {
            var forward = IsAvailable && controls.MoveForward ?
                Movement.POSITIVE :
                Movement.NONE;

            var back = IsAvailable && controls.MoveBack ?
                Movement.NEGATIVE :
                Movement.NONE;

            var right = IsAvailable && controls.MoveRight ?
                Movement.POSITIVE :
                Movement.NONE;

            var left = IsAvailable && controls.MoveLeft ?
                Movement.NEGATIVE :
                Movement.NONE;

            var horizontal = (Movement)((int)right + (int)left);
            var vertical = (Movement)((int)forward + (int)back);

            _playerBody.Move(horizontal, vertical);

            _playerBody.IsRunning = IsAvailable && controls.IsRunning;
            _playerBody.IsJumping = IsAvailable && controls.IsJumping;

            var rotationDelta = new UnityEngine.Vector3(
                IsAvailable ? controls.RotationDeltaX : 0.0f,
                IsAvailable ? controls.RotationDeltaY : 0.0f
            );

            _playerBody.UpdateView(rotationDelta);

            if (IsAvailable && controls.Leave)
            {
                _playerBody.IsSitting = false;
            }
        }
    }
}
