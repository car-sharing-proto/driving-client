using UnityEngine;

namespace Core.Car
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private Seat _driverSeat;
        [SerializeField] private Seat[] _passengerSeats;
        [SerializeField] private Controller[] _controllers;

        [SerializeField] private Wheel _frontRightWheel;
        [SerializeField] private Wheel _frontLeftWheel;
        [SerializeField] private Wheel _rairRightWheel;
        [SerializeField] private Wheel _rairLeftWheel;

        private Engine _engine;

        private Transmission _transmission;

        private void Start()
        {
            _engine = new Engine();
            _transmission = new Transmission();

            _frontLeftWheel.Steer(10);
            _frontRightWheel.Steer(10);
        }

        private void Update()
        {
            _frontLeftWheel.TransmitTorque(Time.deltaTime * 5000);
            _frontRightWheel.TransmitTorque(Time.deltaTime * 5000);
        }
    }
}