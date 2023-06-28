namespace Core.Car
{
    public interface IControls
    {
        public float Gas { get; }
        public float Break { get; }
        public float SteerDelta { get; }
        public bool SetDrivingMode { get; }
        public bool SetParkingMode { get; }
        public bool SetReverseMode { get; }
        public bool SetNeutralMode { get; }
        public bool EngineSwitch { get; }
        public bool ParkingBreakSwitch { get; }
        public bool EmergencySwitch { get; }
        public bool LeftTurnSwitch { get; }
        public bool RightTurnSwitch { get; }
        public bool HeadLightSwitch { get; }
    }
}