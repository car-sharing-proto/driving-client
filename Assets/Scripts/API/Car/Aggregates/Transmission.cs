using UnityEngine;

namespace Core.Car
{
    public class Transmission
    {
        private readonly float[] _gears = new float[]
        {
            4.4f,
            2.726f,
            1.834f,
            1.392f,
            1.0f,
            0.774f
        };

        private readonly float _reverse = 3.44f;

        private readonly float _idlingRMP = 800;
        private readonly float _hydroDamp = 0.95f;
        private readonly float _lastGearRatio = 3.574f;
        private float _hydroTransition = 0;

        public float Torque { get; private set; }
        public float OutputRPM { get; private set; }

        public float GetRatio()
        {
            return _gears[0] * _lastGearRatio;
        }

        public void Update(float inputTorque, float outputRPM)
        {
            var nativeRPM = outputRPM * GetRatio();

            if (nativeRPM < _idlingRMP)
            {
                _hydroTransition = (_idlingRMP - nativeRPM) / _idlingRMP;
            }
            else
            {
                _hydroTransition *= _hydroDamp;
            }

            Torque = inputTorque * GetRatio();
            OutputRPM = Mathf.Lerp(nativeRPM, _idlingRMP, _hydroTransition);
        }
    }
}