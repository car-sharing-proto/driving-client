using UnityEngine;

namespace Core.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterBody : MonoBehaviour, ISitable
    {
        [SerializeField] private Transform _headTransform;
        [SerializeField] private float _climb = 2f;
        [SerializeField] private float _damping = 0.95f;
        [SerializeField] private float _speed = 1;
        [SerializeField] private float _runMultiplier = 2;
        [SerializeField] private float _gravity = 0.1f;
        [SerializeField] private float _jumpForce = 0.3f;

        private CharacterController _characterController;
        private Vector3 _acceleration;
        private Vector3 _planarVelocity;
        private Vector3 _rotation;
        private float _verticalVelocity;

        public Transform HeadTransform => _headTransform;
        public bool IsRunning { get; set; } = false;
        public bool IsJumping { get; set; } = false;
        public bool IsSitting { get; set; } = false;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _planarVelocity = Vector3.zero;
        }

        private void FixedUpdate()
        {
            _characterController.enabled = !IsSitting;

            if (IsSitting)
            {
                ClearEnergy();
                return;
            }

            var isGrounded = IsGrounded();
            var decreaseAcceleration = isGrounded ?
                EnvironmentResistance.Ground :
                EnvironmentResistance.Air;
            var maxVelocity = _speed * (IsRunning ? _runMultiplier : 1f);

            _planarVelocity *= _damping * decreaseAcceleration;
            _planarVelocity += _acceleration * Time.fixedDeltaTime;

            if (_planarVelocity.magnitude > maxVelocity)
            {
                _planarVelocity = _planarVelocity.normalized * maxVelocity;
            }

            if (isGrounded)
            {
                _verticalVelocity = IsJumping ? _jumpForce : 0;
            }
            else
            {
                _verticalVelocity += _gravity * Time.fixedDeltaTime *
                    UnityEngine.Physics.gravity.y;
            }

            _characterController.Move(_planarVelocity + _verticalVelocity * Vector3.up);
        }

        public void Move(Movement horizontal, Movement vertical)
        {
            _acceleration = (
                (int)vertical * transform.forward +
                (int)horizontal * transform.right).normalized * _climb;
        }

        public void UpdateView(Vector3 rotationDelta)
        {
            _rotation += rotationDelta;
            _rotation.y = Mathf.Clamp(_rotation.y, -90, 90);
            _headTransform.localEulerAngles = new Vector3(-_rotation.y, 0);
            transform.localEulerAngles = new Vector3(0, _rotation.x);
        }

        public void SitDown(Transform placePoint)
        {
            IsSitting = true;

            SetParent(placePoint);
        }

        public void StandUp(Transform leavePoint)
        {
            RemoveParent();
            Translate(leavePoint.position);

            IsSitting = false;
        }

        public void Translate(Vector3 position)
        {
            ClearEnergy();
            _characterController.enabled = false;
            transform.position = position;
            _characterController.enabled = true;
        }

        private void ClearEnergy()
        {
            _planarVelocity = Vector3.zero;
            _verticalVelocity = 0;
        }

        private void SetParent(Transform parentTransform)
        {
            transform.SetParent(parentTransform);
            transform.localPosition = Vector3.zero;
        }

        private void RemoveParent()
        {
            transform.SetParent(null);
        }

        private bool IsGrounded()
        {
            return _characterController.isGrounded;
        }
    }
}