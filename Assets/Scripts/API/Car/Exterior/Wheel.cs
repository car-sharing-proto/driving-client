using UnityEngine;

namespace Core.Car
{
    public class Wheel : MonoBehaviour
    {
        [SerializeField] private WheelCollider _collider;
        [SerializeField] private Transform _wheel;
        [SerializeField] private Transform _support;

        private const float c_maxSteerAngle = 30;

        private float _steerAngle = 0; 

        public void Update()
        {
            _collider.GetWorldPose(out Vector3 pos, out Quaternion rot);
            _wheel.SetPositionAndRotation(pos, rot);
            rot.eulerAngles = new Vector3(0, _steerAngle, 0);
            _support.localRotation = rot;
            _support.position = pos;
        }

        public void Steer(float delta)
        {
            _steerAngle += delta;

            if(Mathf.Abs(_steerAngle) > c_maxSteerAngle)
            {
                _steerAngle = Mathf.Sign(_steerAngle) * c_maxSteerAngle;
            }
        }

        public void TransmitTorque(float force)
        {
            _collider.motorTorque = force;
        }

        public void Break(float force)
        {
            _collider.brakeTorque = force;
        }
    }
}
