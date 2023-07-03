using UnityEngine;

namespace Core.Car
{
    public class Seat : MonoBehaviour
    {
        [SerializeField] private Car _car;
        [SerializeField] private bool _isDriverSeat;

        public CarController CarController { get; private set; }
        public bool IsTaken {  get; set; }
        public bool IsDriverSeat => _isDriverSeat;

        private void Awake()
        {
            if(_isDriverSeat)
            {
                // TODO: Remove this cringe.
                CarController = new CarController(_car);
            }
        }
    }
}
