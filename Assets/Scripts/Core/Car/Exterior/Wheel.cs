using UnityEngine;

namespace Core.Car
{
    public class Wheel : MonoBehaviour
    {
        private const float c_minTorqueMultiplier = 0.05f;

        [SerializeField] private WheelCollider _collider;
        [SerializeField] private Transform _wheel;
        [SerializeField] private Transform _support;

        public float SteerAngle { get; set; } = 0;

        public float RPM { get; private set; } = 0;

        private void Update()
        {
            _collider.GetWorldPose(out Vector3 pos, out Quaternion rot);
            _wheel.SetPositionAndRotation(pos, rot);
            _support.localEulerAngles = new Vector3(0, SteerAngle, 0);
            _support.position = pos;

            _collider.steerAngle = SteerAngle;
            _collider.steerAngle = SteerAngle;

            RPM = _collider.rpm;
        }

        public void TransmitTorque(float force)
        {
            _collider.motorTorque = force;
        }

        public void Break(float force)
        {
            _collider.brakeTorque = force;
        }

        private float GetWheelSpeed()
        {
            return Mathf.Abs(2.0f * Mathf.PI *
                _collider.radius * _collider.rpm / 60.0f);
        }
    }
}
