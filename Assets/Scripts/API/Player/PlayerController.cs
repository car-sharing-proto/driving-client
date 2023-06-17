using UnityEngine;

using Movement = Core.Player.PlayerMovement.Movement;

namespace Core.Player
{
    public abstract class PlayerController : MonoBehaviour
    {
        public Movement HorizontalMovement { get; protected set; } = Movement.NONE;
        public Movement VerticalMovement { get; protected set; } = Movement.NONE;
        public bool IsRuning { get; protected set; } = false;
        public bool IsJumping { get; protected set; } = false;
        public Vector3 RotationDelta { get; protected set; } = Vector3.zero;

        private void Update()
        {
            GetInput();
        }

        protected abstract void GetInput();
    }
}