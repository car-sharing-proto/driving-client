using UnityEngine;

namespace Core.Car
{
    public class Seat : MonoBehaviour
    {
        [SerializeField] private Car _car;
        [SerializeField] private bool _isDriverSeat;

        public bool IsTaken {  get; set; }
        public bool IsDriverSeat => _isDriverSeat;
        public Car Car => _car;
    }
}
