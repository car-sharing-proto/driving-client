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

        private float _velocity = 0;
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

        private void FixedUpdate()
        {
            var t = Time.fixedDeltaTime;

            _newPosition = _target.position;
            //_oldPosition = Vector3.Lerp(_oldPosition, _newPosition, t);
            _newVelocity = (_newPosition - _oldPosition) / t;
            //_oldVelocity = Vector3.Lerp(_oldVelocity, _newVelocity, t);
            // Debug.Log(_newVelocity);
            _displace = (_newVelocity - _oldVelocity) / t;
            //Debug.Log(_displace);
            _oldPosition = _target.position;
            _oldVelocity = _newVelocity;
            _velocity = _newVelocity.magnitude * t;
            _shift += c_speed * t * _displace;


            transform.position = _target.position - _displace * 0.01f;

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