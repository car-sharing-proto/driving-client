using UnityEngine;

namespace Core.Car
{
    public class SteeringWheel : MonoBehaviour
    {
        private const float c_maxSteerAngle = 30;

        [SerializeField] private Transform _wheelTransform;

        public float SteerAngle { get; private set; } = 0;

        private void Update()
        {
            _wheelTransform.localEulerAngles = 
                new Vector3(-SteerAngle / c_maxSteerAngle * 360.0f * 1.5f, 0);
        }

        public void Steer(float delta)
        {
            SteerAngle += delta * GetStiffness();

            if (Mathf.Abs(SteerAngle) > c_maxSteerAngle)
            {
                SteerAngle = Mathf.Sign(SteerAngle) * c_maxSteerAngle;
            }
        }

        private float GetStiffness()
        {
            return 20; // (2 + 2 * Mathf.Abs(SteerAngle));
        }
    }
}