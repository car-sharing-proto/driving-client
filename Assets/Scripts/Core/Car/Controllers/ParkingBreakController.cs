using UnityEngine;

namespace Core.Car
{
    public class ParkingBreakController : Controller
    {
        [SerializeField] private ParkingBreak _parkingBreak;

        public override bool Check()
        {
            return _parkingBreak.State != ParkingBreakState.LOWERED;
        }
    }
}