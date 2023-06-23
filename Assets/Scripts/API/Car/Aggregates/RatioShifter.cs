using UnityEngine;

namespace Core.Car
{
    public class RatioShifter
    {
        private float _shiftSpeed;
        private float _nextRatio;
        private float _prevRatio;
        private float _t;

        public float Value { get; private set; }
        public bool IsShifting => _t > 0f && _t < 1f;

        public RatioShifter(float initialRatio)
        {
            _nextRatio = initialRatio;
            _shiftSpeed = 0;
            _prevRatio = 0;
            _t = 1f;
        }

        public void Shift(float nextRatio, float shiftSpeed)
        {
            _prevRatio = Value;
            _nextRatio = nextRatio;
            _shiftSpeed = shiftSpeed;

            _t = 0;
        }

        public void Update()
        {
            if (_t < 1)
            {
                _t += _shiftSpeed;
            }

            Value =  Mathf.Lerp(_prevRatio, _nextRatio, _t);
        }
    }
}
