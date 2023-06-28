using UnityEngine;

namespace Core.Car
{
    [System.Serializable]
    public class Engine
    {
        [SerializeField] private AnimationCurve _resistanceCurve;
        [SerializeField] private Starter _starter;
        [SerializeField] private float _maxRPM;
        [SerializeField] private float _idlingRPM;
        [SerializeField] private float _maxTorque;

        private float _baseGas;

        public Starter Starter => _starter;
        public bool Enabled => _starter.State == EngineState.STARTED;
        public float MaxRPM => _maxRPM;
        public float MaxTorque => _maxTorque;

        public float RPM { get; private set; } 
        public float OutputRPM { get; private set; } 
        public float Torque { get; private set; } 

        public void Initialize()
        {
            _baseGas = _idlingRPM / MaxRPM;
        }

        public void Update(float inputGas, float outputRPM, float load)
        {
            _starter.Update();
            UpdateRPM(inputGas, outputRPM, load);
        }

        private void UpdateRPM(float inputGas, float outputRPM, float load)
        {
            if (outputRPM > MaxRPM)
            {
                outputRPM = MaxRPM;
            }

            var localGas = _baseGas + (inputGas * (1.0f - _baseGas));
            var feedback = outputRPM * load;
            var inputResistance =
                1.0f - _resistanceCurve.Evaluate(feedback / MaxRPM);
            var inputRPM = localGas * MaxRPM * _starter.RPMValue;
            var torqueRPM = (inputRPM - feedback) * inputResistance;
            Torque = torqueRPM / MaxRPM * MaxTorque;
            OutputRPM = inputRPM;
            RPM = outputRPM;
        }
    }
}
