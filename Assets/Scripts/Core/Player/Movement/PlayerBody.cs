using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerBody : MonoBehaviour, ISitting
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

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _planarVelocity = Vector3.zero;
        }

        private void Update()
        {
            if (IsSitting)
            {
                ClearEnergy();
                return;
            }

            var isGrounded = IsGrounded();
            var decreasing = isGrounded ?
                EnvironmentResistance.Ground :
                EnvironmentResistance.Air;
            var maxVelocity = _speed *
                (IsRunning ? _runMultiplier : 1.0f);  

            _planarVelocity *= Mathf.Pow(
                _damping * decreasing, Time.deltaTime);
            _planarVelocity += _acceleration * Time.deltaTime;

            if (_planarVelocity.magnitude > maxVelocity)
            {
                _planarVelocity = 
                    _planarVelocity.normalized * maxVelocity;
            }

            if (isGrounded)
            {
                _verticalVelocity = IsJumping ? _jumpForce : 0.0f;
            }
            else
            {
                _verticalVelocity += _gravity *
                    Time.deltaTime * Physics.gravity.y;
            }

            _characterController.Move(Time.deltaTime * _planarVelocity 
                + Time.deltaTime * _verticalVelocity * Vector3.up);
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
            _characterController.enabled = false;

            SetParent(placePoint);
        }

        public void StandUp(Transform leavePoint)
        {
            RemoveParent();
            Translate(leavePoint.position);

            IsSitting = false;
            _characterController.enabled = true;
        }

        public void Translate(Vector3 position)
        {
            ClearEnergy();
            transform.position = position;
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