using System;
using UnityEngine;

namespace Core.Car
{
    public enum TransmissionMode
    {
        NEUTRAL,
        DRIVING,
        REVERSE,
        PARKING
    }

    [System.Serializable]
    public class Transmission
    {
        private const float c_speedEps = 0.1f;

        [SerializeField] private AnimationCurve _fluidCouplingCurve;
        [SerializeField] private Gear[] _gears;
        [SerializeField] private float _reverseGearRatio = 3.44f;
        [SerializeField] private float _fluidDamp = 0.9999f;
        [SerializeField] private float _idlingRMP = 800;
        [SerializeField] private float _lastGearRatio = 3.574f;

        [SerializeField] private float _accelerationFactor = 1;

        private RatioShifter _ratioShifter;
        private float _fluidTransition = 0;
        private float _speed = 0;
        private int _currentGear = 0;

        public Action<TransmissionMode> OnModeChange;

        public bool Lock { get; set; }
        public TransmissionMode Mode { get; private set; }
        public float Torque { get; private set; }
        public float RPM { get; private set; }
        public float Load { get; private set; }
        public float Break { get; private set; }
        public int CurrentGear => _currentGear;

        public void Initialize()
        {
            _ratioShifter = new RatioShifter(_gears[0].Ratio);

            Mode = TransmissionMode.PARKING;
        }

        public float GetRatio()
        {
            return Mode switch
            {
                TransmissionMode.REVERSE =>
                    -_reverseGearRatio * _lastGearRatio,
                TransmissionMode.DRIVING =>
                    _ratioShifter.Value * _lastGearRatio,
                _ => 0,
            };
        }

        public void SwitchMode(TransmissionMode mode)
        {
            if (Lock)
            {
                return;
            }

            if(mode == Mode)
            {
                return;
            }

            if (Mathf.Abs(_speed) <= c_speedEps)
            {
                Mode = mode;

                OnModeChange?.Invoke(Mode);
            }
        }

        public void Update(float inputTorque, float inputRPM,
            float outputRPM, float speed)
        {
            _speed = speed;
            _ratioShifter.Update();

            UpdateTorque(inputTorque, inputRPM, outputRPM);
            UpdateGearShifting(outputRPM);
            UpdateBreak();
        }

        private void UpdateBreak()
        {
            Break = Mode == TransmissionMode.PARKING ? 1.0f : 0.0f;
        }

        private void UpdateTorque(float inputTorque,
            float inputRPM, float outputRPM)
        {
            var nativeRPM = outputRPM * GetRatio();

            if (nativeRPM < _idlingRMP)
            {
                _fluidTransition = _fluidCouplingCurve.Evaluate(
                   (_idlingRMP - nativeRPM) / _idlingRMP);
            }
            else
            {
                _fluidTransition *= _fluidDamp;
            }

            Load = 1.0f - _fluidTransition;
            Torque = inputTorque * GetRatio();
            RPM = Mathf.Lerp(nativeRPM, inputRPM, _fluidTransition);
        }

        private void UpdateGearShifting(float rpm)
        {
            if (Mode != TransmissionMode.DRIVING)
            {
                _currentGear = 0;

                return;
            }

            rpm *= GetRatio();

            if (_ratioShifter.IsShifting)
            {
                return;
            }
            if (rpm > _gears[_currentGear].MaxRPM * _accelerationFactor)
            {
                UpshiftGear(1);
            }
            else if (rpm < _gears[_currentGear].MinRPM * _accelerationFactor)
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
                if (_gears[_currentGear + 1].MinSpeed > _speed)
                {
                    return;
                }

                _currentGear += count;
                _ratioShifter.Shift(
                    _gears[_currentGear].Ratio,
                    _gears[_currentGear].ShiftSpeed);
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
                _ratioShifter.Shift(
                    _gears[_currentGear].Ratio,
                    _gears[_currentGear].ShiftSpeed);
            }
        }
    }
}