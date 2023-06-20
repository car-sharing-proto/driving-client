using UnityEngine;

namespace Core.Car
{
    public class SteeringWheel : MonoBehaviour
    {
        [SerializeField] private Transform _wheelTransform;

        private const float c_maxSteerAngle = 30;

        public float SteerAngle { get; private set; } = 0;

        private void Update()
        {
            var angle = _wheelTransform.localEulerAngles;
            angle.x = SteerAngle / c_maxSteerAngle * 360.0f * 1.5f;
            _wheelTransform.localEulerAngles = angle;
        }

        public void Steer(float delta)
        {
            SteerAngle += delta;

            if (Mathf.Abs(SteerAngle) > c_maxSteerAngle)
            {
                SteerAngle = Mathf.Sign(SteerAngle) * c_maxSteerAngle;
            }
        }
    }
}