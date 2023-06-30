using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Core.Inertional
{
    public class SmoothFollow : MonoBehaviour
    {
        [SerializeField] private float _smoothShift = 0.85f;
        [SerializeField] private float _maxShift = 5.0f;
        [SerializeField] private Transform _target;

        private Vector3 _displace;
        private Vector3 _shift;
        private Vector3 _newPosition, _oldPosition;
        private Vector3 _newVelocity, _oldVelocity;

        private Vector3 _velocity;
        private const float c_speed = 0.05f;
        private const float c_harshness = 1000.0f;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;

            Start();
        }

        private void Start()
        {
            _newPosition = _target.position;
            _oldPosition = _newPosition;
        }

        private void Update()
        {
            //transform.position = 
            //    Vector3.SmoothDamp(
            //    transform.position,
            //    _target.position, 
            //    ref _velocity,
            //    0.0f,
            //    100);

            //return;
            var t = Time.deltaTime;

            _newPosition = _target.position;

            _newVelocity = (_newPosition - _oldPosition) / t;
            _displace = (_newVelocity - _oldVelocity) / t;

            _oldVelocity = _newVelocity;
            _velocity += (_displace );
            _shift += (_newPosition - _oldPosition);
            _shift *= 0.95f;
            _oldPosition = _target.position;
            //_velocity *= 0.95f;

            //Debug.Log($"v {_newVelocity}");
            //Debug.Log($"a {_displace}");
            //Debug.Log($"s {_shift}");

            transform.position = _target.position - _shift * 0.001f;

            //CeilShift();

            //transform.position = _target.position - _shift;
            //_shift *= _smoothShift + Limit(_velocity) * (0.999f - _smoothShift);
        }

        private void CeilShift()
        {
            if (_shift.magnitude >= _maxShift)
                _shift = _shift.normalized * _maxShift;
        }

        private float Limit(float x)
        {
            return 1.0f - 1.0f / (1.0f + x * x * x * c_harshness);
        }
    }
}