using UnityEngine;

namespace Core.Cameras
{
    public class SideCamera : MovableCamera
    {
        private const float c_minBeta = 2f;
        private const float c_maxBeta = 3f;

        private float _alpha;
        private float _beta;
        private float _ro;

        [SerializeField] private Transform _target;
        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private float _sensitivity = 0.125f;
        [SerializeField] private float _minRadius = 3;
        [SerializeField] private float _maxRadius = 10;

        private void Awake()
        {
            _ro = _minRadius;
        }

        private void FixedUpdate()
        {
            UpdateOffset();
            UpdatePosition();
        }

        private void UpdateOffset()
        {
            if (!IsMovable) return;

            float dx = Input.GetAxis("Mouse X") * _sensitivity;
            float dy = Input.GetAxis("Mouse Y") * _sensitivity;
            float dz = Input.mouseScrollDelta.y;

            _alpha += dx;
            _beta += dy;
            _ro -= dz;

            if (_beta < c_minBeta)
                _beta = c_minBeta;
            if (_beta > c_maxBeta)
                _beta = c_maxBeta;

            if (_ro < _minRadius)
                _ro = _minRadius;
            if (_ro > _maxRadius)
                _ro = _maxRadius;
        }
        private void UpdatePosition()
        {
            float sina = Mathf.Sin(_alpha);
            float cosa = Mathf.Cos(_alpha);
            float sinb = Mathf.Sin(_beta);
            float cosb = Mathf.Cos(_beta);

            Vector3 desiredPosition = _target.position + 
                (new Vector3(sina * cosb, sinb, cosb * cosa) * _ro);
            Vector3 smoothedPosition = 
                Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(_target);
        }
    }
}