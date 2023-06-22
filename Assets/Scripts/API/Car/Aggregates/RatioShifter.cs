using UnityEngine;

namespace Core.Car
{
    public class RatioShifter
    {
        private readonly float _shiftSpeed;
        private float _nextRatio;
        private float _prevRatio;
        private float _t;

        public float Value { get; private set; }
        public bool IsShifting => _t > 0f && _t < 1f;

        public RatioShifter(float initialRatio, float shiftSpeed)
        {
            _nextRatio = initialRatio;
            _shiftSpeed = shiftSpeed;
            _prevRatio = 0;
            _t = 1f;
        }

        public void Shift(float prevRatio, float nextRatio)
        {
            _prevRatio = prevRatio;
            _nextRatio = nextRatio;
            _t = 0;
        }

        public void Shift(float nextRatio)
        {
            _prevRatio = Value;
            _nextRatio = nextRatio;
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
