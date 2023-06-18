using UnityEngine;

using Movement = Core.Player.PlayerMovement.Movement;

namespace Core.Player
{
    public abstract class PlayerController : MonoBehaviour, Car.ISitable
    {
        [SerializeField] private PlayerMovement _playerMovement;

        public Movement HorizontalMovement { get; protected set; } = Movement.NONE;
        public Movement VerticalMovement { get; protected set; } = Movement.NONE;
        public Vector3 RotationDelta { get; protected set; } = Vector3.zero;
        public bool IsRuning { get; protected set; } = false;
        public bool IsJumping { get; protected set; } = false;
        public bool IsSitting { get; protected set; } = false;

        private void Update()
        {
            GetInput();
            ApplyInput();
        }

        private void ApplyInput()
        {
            _playerMovement.UpdateInput(
                HorizontalMovement,
                VerticalMovement,
                IsRuning,
                IsJumping,
                IsSitting,
                RotationDelta);
        }

        public virtual void SitDown(Transform placePoint)
        {
            IsSitting = true;

            ApplyInput();

            _playerMovement.SetParent(placePoint);
        }

        public virtual void StandUp(Transform leavePoint)
        {
            _playerMovement.RemoveParent();
            _playerMovement.Translate(leavePoint.position);

            IsSitting = false;

            ApplyInput();
        }

        protected abstract void GetInput();
    }
}