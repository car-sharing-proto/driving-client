using UnityEngine;

namespace Core.Car
{
    [RequireComponent(typeof(Rigidbody))]
    public class Car : MonoBehaviour
    {
        [SerializeField] private Seat _driverSeat;
        [SerializeField] private Seat[] _passengerSeats;
        [SerializeField] private Controller[] _controllers;

        [SerializeField] private SteeringWheel _steeringWheel;

        [SerializeField] private Wheel _frontRightWheel;
        [SerializeField] private Wheel _frontLeftWheel;
        [SerializeField] private Wheel _rearRightWheel;
        [SerializeField] private Wheel _rearLeftWheel;

        [SerializeField] private Tachometer _tachometer;

        [SerializeField] private Pedal _gasPedal;
        [SerializeField] private Pedal _breakPedal;

        [SerializeField] private float _breakForce;

        [SerializeField] private float _maxSpeed;

        public const float c_velocityEps = 0.01f;

        private Rigidbody _rigidbody;

        private Engine _engine;
        private Transmission _transmission;

        public Pedal GasPedal => _gasPedal;
        public Pedal BreakPedal => _breakPedal;

        private void Awake()
        {
            _engine = new Engine(900.0f, 6000.0f, 180.0f);
            _transmission = new Transmission();
            _tachometer.SetEngine(_engine);
            _rigidbody = GetComponent<Rigidbody>(); 

            //_steeringWheel.Steer(-5);
        }

        private void FixedUpdate()
        {
            var resistance = 0;// GetResistanceForce();
            _frontLeftWheel.TransmitTorque(_transmission.Torque - resistance);
            _frontRightWheel.TransmitTorque(_transmission.Torque - resistance);
            _frontLeftWheel.SteerAngle = _steeringWheel.SteerAngle;
            _frontRightWheel.SteerAngle = _steeringWheel.SteerAngle;

            var wheelsRPM = (_frontLeftWheel.RPM + _frontRightWheel.RPM) * 0.5f;

            _engine.Update(_gasPedal.Value, _transmission.OutputRPM);
            _transmission.Update(_engine.Torque, wheelsRPM);

            _frontLeftWheel.Break(_breakPedal.Value * _breakForce);
            _frontRightWheel.Break(_breakPedal.Value * _breakForce);
            _rearLeftWheel.Break(_breakPedal.Value * _breakForce);
            _rearRightWheel.Break(_breakPedal.Value * _breakForce);
        }

        private float GetResistanceForce()
        {
            var v = GetVelocity();
            var airResistance = _engine.MaxTorque / (_maxSpeed * _maxSpeed);

            if (Mathf.Abs(v) < c_velocityEps)
                v = 0;

            return airResistance * v * v * Mathf.Sign(v);
        }

        public float GetVelocity()
        {
            var project = Vector3.Project(_rigidbody.velocity,
                _rigidbody.transform.forward);
            var movementSign = Mathf.Sign(project.x / 
                _rigidbody.transform.forward.x);
            var v = project.magnitude;

            return v * movementSign;
        }
    }
}