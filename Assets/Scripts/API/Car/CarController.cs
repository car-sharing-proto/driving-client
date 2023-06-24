namespace Core.Car
{
    public class CarController
    {
        private readonly Car _car;

        public CarController(Car car)
        {
            this._car = car;
        }

        public void EngineSwitch()
        {
            var state =
                _car.Engine.Starter.State == EngineState.STARTED ?
                EngineState.STOPED :
                EngineState.STARTED;

            _car.Engine.Starter.SetState(state);
        }

        public void GasPedalPress(float value)
        {
            _car.GasPedal.Value = value;
        }

        public void BreakPedalPress(float value)
        {
            _car.BreakPedal.Value = value;
        }

        public void SteerLeft(float speed)
        {
            _car.SteeringWheel.Steer(-speed);
        }

        public void SteerRight(float speed)
        {
            _car.SteeringWheel.Steer(speed);
        }

        public void SetParkingMode()
        {
            _car.Transmission.SwitchMode(TransmissionMode.PARKING);
        }

        public void SetDrivingMode()
        {
            _car.Transmission.SwitchMode(TransmissionMode.DRIVING);
        }

        public void SetReverseMode()
        {
            _car.Transmission.SwitchMode(TransmissionMode.REVERSE);
        }
    }
}