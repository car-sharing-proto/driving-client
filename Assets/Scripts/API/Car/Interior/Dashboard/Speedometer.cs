using UnityEngine;

namespace Core.Car
{
    public class Speedometer : MonoBehaviour
    {
        [SerializeField] private Transform _needle;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _maxSpeed;

        public void UpdateValue(float speed)
        {
            var ratio = Mathf.Abs(speed) / _maxSpeed;

            if (ratio > 1)
            {
                ratio = 1;
            }

            if (ratio < 0)
            {
                ratio = 0;
            }

            _needle.localEulerAngles =
                _maxAngle * ratio * Vector3.right;
        }
    }
}
