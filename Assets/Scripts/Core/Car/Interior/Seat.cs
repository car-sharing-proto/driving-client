using UnityEngine;

namespace Core.Car
{
    public class Seat : MonoBehaviour
    {
        [SerializeField] private Car _car;
        [SerializeField] private bool _isDriverSeat;

        public bool IsTaken {  get; set; }

        public Car ProvideControllableCar() 
        {
            if (!_isDriverSeat)
            {
                return null;
            }

            return _car;
        }
    }
}
