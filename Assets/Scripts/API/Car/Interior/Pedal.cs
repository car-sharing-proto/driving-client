using UnityEngine;

namespace Core.Car
{
    public class Pedal : MonoBehaviour
    {
        [SerializeField] private Vector3 _minAngle;
        [SerializeField] private Vector3 _maxAngle;
        private readonly float _minValue = 0;
        private readonly float _maxValue = 1;
        private float _value = 0;

        public bool IsPressed => _value != _minValue;
        public bool IsFullyPressed => _value == _maxValue;
        public float Value
        {
            set
            {
                if (value < _minValue)
                {
                    this._value = _minValue;
                }
                else if (value > _maxValue)
                {
                    this._value = _maxValue;
                }
                else
                {
                    this._value = value;
                }
            }

            get
            {
                return _value;
            }
        }

        private void Update()
        {
            transform.localEulerAngles = 
                Vector3.Lerp(_minAngle, _maxAngle, 
                (_value - _minValue) / (_maxValue - _minValue));
        }
    }
}
