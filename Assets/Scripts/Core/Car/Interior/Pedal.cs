using UnityEngine;

namespace Core.Car
{
    public class Pedal : MonoBehaviour
    {
        [SerializeField] private Vector3 _minAngle;
        [SerializeField] private Vector3 _maxAngle;
        private float _value = 0;

        public bool IsPressed => _value != 0.0f;
        public bool IsFullyPressed => _value == 1.0f;

        public float Value
        {
            set
            {
                _value = Mathf.Clamp01(value);
            }

            get
            {
                return _value;
            }
        }

        private void Update()
        {
            transform.localEulerAngles =
                Vector3.Lerp(_minAngle, _maxAngle, _value);
        }
    }
}
