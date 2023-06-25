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
            if(!IsAvailable) return;

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

            if (controls.ParkingBreakSwitch)
            {
                _car.ParkingBreak.Switch();
            }

            if (controls.EngineSwitch)
            {
                var state =
                _car.Engine.Starter.State == EngineState.STARTED ?
                EngineState.STOPED :
                EngineState.STARTED;

                _car.Engine.Starter.SetState(state);
            }
        }
    }
}