namespace Core.Car
{
    public class CarController
    {
        private readonly Car _car;

        public bool IsAvailable { get; set; }
        public Car Car => _car;

        public CarController(Car car)
        {
            this._car = car;
        }

        public void Update(IControls controls)
        {
            if (!IsAvailable) return;

            _car.GasPedal.Value = controls.Gas;
            _car.BreakPedal.Value = controls.Break;
            _car.SteeringWheel.Steer(controls.SteerDelta);

            if (controls.SetDrivingMode)
            {
                _car.Transmission.SwitchMode(TransmissionMode.DRIVING);
            }

            if (controls.SetParkingMode)
            {
                _car.Transmission.SwitchMode(TransmissionMode.PARKING);
            }

            if (controls.SetReverseMode)
            {
                _car.Transmission.SwitchMode(TransmissionMode.REVERSE);
            }

            if (controls.SetNeutralMode)
            {
                _car.Transmission.SwitchMode(TransmissionMode.NEUTRAL);
            }

            if (controls.ParkingBreakSwitch)
            {
                _car.ParkingBreak.Switch();
            }

            if (controls.LeftTurnSwitch)
            {
                _car.TurnLights.SwitchLeft();
            }

            if (controls.RightTurnSwitch)
            {
                _car.TurnLights.SwitchRight();
            }

            if (controls.EmergencySwitch)
            {
                _car.TurnLights.SwitchEmergency();
            }

            if (controls.HeadLightSwitch)
            {
                _car.HeadLights.Switch();
            }

            if (controls.EngineSwitch)
            {
                _car.Engine.Starter.SwitchState();
            }
        }
    }
}