using UnityEngine;

namespace Core.Car
{
    public enum EngineState
    {
        STOPED,
        STARTED
    }

    public class Engine
    {
        private readonly float _idlingRPM;
        private readonly float _baseGas;

        public float MaxTorque { get; }
        public float RPM { get; private set; } 
        public float Torque { get; private set; } 
        public float MaxRPM { get; }
        public EngineState State { get; set; }

        public Engine(float idlingRPM, float maxRPM, float maxTorque)
        {
            MaxRPM = maxRPM;
            MaxTorque = maxTorque;

            _idlingRPM = idlingRPM;
            _baseGas = _idlingRPM / MaxRPM;
        }

        public void Update(float inputGas, float outputRPM)
        {
            var localGas = (_baseGas + (inputGas * (1.0f - _baseGas)));

            var inputResistance = 1 - outputRPM / MaxRPM;
            var outputTorque = outputRPM / MaxRPM * MaxTorque;
            var inputTorque = localGas * MaxTorque;
            Torque = (inputTorque - outputTorque) * inputResistance;

            RPM = outputRPM;
        }
    }
}
