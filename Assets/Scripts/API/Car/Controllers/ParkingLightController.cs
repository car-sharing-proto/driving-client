using UnityEngine;

namespace Core.Car
{
    public class ParkingLightController : Controller
    {
        [SerializeField] private Car _car;

        public override bool Check()
        {
            return _car.Engine.Enabled;
        }
    }
}

