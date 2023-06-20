using UnityEngine;

namespace Core.Car
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private Seat _driverSeat;
        [SerializeField] private Seat[] _passengerSeats;
        [SerializeField] private Door _trunkDoor;

        [SerializeField] private Controller[] _controllers;

        private Engine _engine;

        private Transmission _transmission;

        private void Start()
        {
            _engine = new Engine();
            _transmission = new Transmission();
        }
    }
}