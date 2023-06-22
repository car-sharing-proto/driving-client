using UnityEngine;

namespace Core.Car
{
    public enum TransmissionMode : int
    {
        NEUTRAL = 0,
        DRIVE = 1,
        REVERSE = -1,
        PARKING = 0
    }

    public class Transmission
    {
        private readonly RatioShifter _ratioShifter;
        private readonly float[] _gears = new float[]
        {
            4.4f,
            2.726f,
            1.834f,
            1.392f,
            1.0f,
            0.774f
        };

        private readonly float _reverseGear = 3.44f;

        private readonly float _hydroDamp = 0.95f;
        private readonly float _idlingRMP = 800;
        private readonly float _lastGearRatio = 3.574f;
        private readonly float _downshiftThreshold = 1500.0f;
        private readonly float _superDownshiftThreshold = 1200.0f;
        private readonly float _upshiftThreshold = 2500.0f;
        private float _hydroTransition = 0;
        private int _currentGear = 0;

        public TransmissionMode Mode { get; private set; }
        public float Torque { get; private set; }
        public float OutputRPM { get; private set; }
        public int CurrentGear => _currentGear;

        public Transmission()
        {
            Mode = TransmissionMode.DRIVE;
            _ratioShifter = new RatioShifter(_gears[0], 0.05f);
        }

        public Transmission(
            float[] gears,
            float reverseGear,
            float hydroDamp,
            float idlingRMP,
            float lastGearRatio,
            float downshiftThreshold,
            float superDownshiftThreshold,
            float upshiftThreshold,
            float hydroTransition
        ) : base()
        {
            _gears = gears;
            _reverseGear = reverseGear;
            _hydroDamp = hydroDamp;
            _idlingRMP = idlingRMP;
            _lastGearRatio = lastGearRatio;
            _downshiftThreshold = downshiftThreshold;
            _superDownshiftThreshold = superDownshiftThreshold;
            _upshiftThreshold = upshiftThreshold;
            _hydroTransition = hydroTransition;
        }

        public float GetRatio()
        {
            return Mode switch
            {
                TransmissionMode.REVERSE =>
                    _reverseGear * _lastGearRatio,
                TransmissionMode.DRIVE =>
                    _ratioShifter.Value * _lastGearRatio,
                _ => 0,
            };
        }

        public void Update(float inputTorque, float outputRPM)
        {
            UpdateTorque(inputTorque, outputRPM);
            UpdateGearShifting();
        }

        private void UpdateTorque(float inputTorque, float outputRPM)
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

        private void UpdateGearShifting()
        {
            _ratioShifter.Update();

            if (_ratioShifter.IsShifting)
            {
                return;
            }
            if (OutputRPM > _upshiftThreshold)
            {
                UpshiftGear(1);
            }
            else if (OutputRPM < _superDownshiftThreshold)
            {
                DownshiftGear(2);
            }
            else if(OutputRPM < _downshiftThreshold)
            {
                DownshiftGear(1);
            }
        }

        private void UpshiftGear(int count)
        {
            if (count <= 0)
            {
                throw new System.ArgumentException();
            }

            if (_currentGear < _gears.Length - count)
            {
                _currentGear += count;
                _ratioShifter.Shift(_gears[_currentGear]);
            }
        }

        private void DownshiftGear(int count)
        {
            if (count <= 0)
            {
                throw new System.ArgumentException();
            }

            if (_currentGear > count - 1)
            {
                _currentGear -= count;
                _ratioShifter.Shift(_gears[_currentGear]);
            }
        }
    }
}