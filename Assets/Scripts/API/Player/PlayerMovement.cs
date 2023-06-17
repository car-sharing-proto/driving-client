using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        public enum Movement : int
        {
            POSITIVE = 1,
            NEGATIVE = -1,
            NONE = 0
        }

        [SerializeField] private Transform _headTransform;

        [SerializeField] private float _gravityScale = 0.3f;
        [SerializeField] private float _maxSpeed = 10;
        [SerializeField] private float _jumpForce = 10;
        [SerializeField] private float _runMultiplier = 1.5f;
        [SerializeField] private float _acceleration = 1f;

        private Movement _horizontalMovement = Movement.NONE;
        private Movement _verticalMovement = Movement.NONE;
        private bool _isRuning = false;
        private bool _isJumping = false;

        private PlayerController _controller;
        private CharacterController _characterController;

        private Vector3 _rotationDelta = Vector3.zero;
        private Vector3 _velocityPlanar = Vector3.zero;
        private float _velocityVertical = 0f;

        public void SetController(PlayerController controller) => _controller = controller;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            UpdateInput();
            UpdateView();
        }
        private void FixedUpdate()
        {
            UpdateMove();
            CalculateVelocity();
        }

        private void UpdateView()
        {
            _rotationDelta.y = Mathf.Clamp(_rotationDelta.y, -90, 90);
            _headTransform.localEulerAngles = new Vector3(-_rotationDelta.y, 0);
            transform.eulerAngles = new Vector3(0, _rotationDelta.x);
        }

        private void UpdateInput()
        {
            if (!_controller) return;

            _horizontalMovement = _controller.HorizontalMovement;
            _verticalMovement = _controller.VerticalMovement;
            _isRuning = _controller.IsRuning;
            _isJumping = _controller.IsJumping;
            _rotationDelta += _controller.RotationDelta;
        }

        private void CalculateVelocity()
        {
            var isGrounded = IsGrounded();
            var runMultiplier = _isRuning ? _runMultiplier : 1f;
            var decreaseAcceleration = isGrounded ? Physics.Resistance.Ground : Physics.Resistance.Air;

            _velocityPlanar += (int)_horizontalMovement * runMultiplier *
                _acceleration * Time.fixedDeltaTime * transform.right;

            _velocityPlanar += (int)_verticalMovement * runMultiplier *
                _acceleration * Time.fixedDeltaTime * transform.forward;

            PlanarCeil(ref _velocityPlanar, _isRuning ? _maxSpeed * runMultiplier : _maxSpeed);

            if (_verticalMovement == Movement.NONE && _horizontalMovement == Movement.NONE)
                _velocityPlanar *= decreaseAcceleration;

            if (IsGrounded())
            {
                _velocityVertical = _isJumping ? _jumpForce : 0;
            }
            else
            {
                _velocityVertical += _gravityScale * Time.fixedDeltaTime 
                    * UnityEngine.Physics.gravity.y;
            }
        }

        private bool IsGrounded()
        {
            float eps = 0.3f;
            return UnityEngine.Physics.Raycast(transform.position, -GetNormal(),
                _characterController.height / 2.0f + eps);
        }
        private Vector3 Project(Vector3 vector, Vector3 normal)
        {
            return vector - Vector3.Dot(vector, normal) * normal;
        }

        private Vector3 GetNormal()
        {
            if (UnityEngine.Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
            {
                return hit.normal;
            }

            return Vector3.zero;
        }

        private void PlanarCeil(ref Vector3 vector, float maxMagnitude)
        {
            var planarMovent = new Vector2(vector.x, vector.z);

            if (planarMovent.magnitude > maxMagnitude)
            {
                planarMovent = planarMovent.normalized * maxMagnitude;

                vector.x = planarMovent.x;
                vector.z = planarMovent.y;
            }
        }

        private void UpdateMove()
        {
            Vector3 normal = GetNormal();
            Vector3 moveDirection = Project(_velocityPlanar, normal) + Vector3.up * _velocityVertical;

            _characterController.Move(moveDirection);
        }
    }
}