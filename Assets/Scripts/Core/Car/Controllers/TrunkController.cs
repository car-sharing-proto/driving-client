using UnityEngine;

namespace Core.Car
{
    public class TrunkController : Controller
    {
        [SerializeField] private Door _trunkDoor;
        public override bool Check()
        {
            return _trunkDoor.State != IOpenable.OpenState.CLOSED;
        }
    }
}