namespace Core.Car
{
    public class CarController
    {
        private readonly IControls _controls;

        private Car _car;

        public bool IsAvailable { get; set; }

        public Car Car => _car;

        public CarController(IControls _controls)
        {
            this._controls = _controls;

            IsAvailable = true;
        }

        public void SetCar(Car car)
        {
            if(car == null)
            {
                throw new System.NullReferenceException();
            }

            this._car = car;
        }

        public void RemoveCar()
        {
            this._car = null;
        }

        public void Update()
        {
            if (!IsAvailable) return;

            if(_car == null) return;

            _car.GasPedal.Value = _controls.Gas;
            _car.BreakPedal.Value = _controls.Break;
            _car.SteeringWheel.Steer(_controls.SteerDelta);

            if (_controls.SetDrivingMode)
            {
                _car.Transmission.SwitchMode(TransmissionMode.DRIVING);
            }

            if (_controls.SetParkingMode)
            {
                _car.Transmission.SwitchMode(TransmissionMode.PARKING);
            }

            if (_controls.SetReverseMode)
            {
                _car.Transmission.SwitchMode(TransmissionMode.REVERSE);
            }

            if (_controls.SetNeutralMode)
            {
                _car.Transmission.SwitchMode(TransmissionMode.NEUTRAL);
            }

            if (_controls.ParkingBreakSwitch)
            {
                _car.ParkingBreak.Switch();
            }

            if (_controls.LeftTurnSwitch)
            {
                _car.TurnLights.SwitchLeft();
            }

            if (_controls.RightTurnSwitch)
            {
                _car.TurnLights.SwitchRight();
            }

            if (_controls.EmergencySwitch)
            {
                _car.TurnLights.SwitchEmergency();
            }

            if (_controls.HeadLightSwitch)
            {
                _car.HeadLights.Switch();
            }

            if (_controls.EngineSwitch)
            {
                _car.Engine.Starter.SwitchState();
            }
        }
    }
}