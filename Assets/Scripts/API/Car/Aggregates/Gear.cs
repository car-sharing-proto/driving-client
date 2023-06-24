using UnityEngine;

namespace Core.Car
{
    [System.Serializable]
    public class Gear
    {
        [SerializeField] private float _ratio;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _shiftSpeed;
        [SerializeField] private float _minRPM;
        [SerializeField] private float _maxRPM;

        public float Ratio => _ratio;
        public float MinSpeed => _minSpeed;
        public float MinRPM => _minRPM;
        public float MaxRPM => _maxRPM;
        public float ShiftSpeed => _shiftSpeed;
    }
}