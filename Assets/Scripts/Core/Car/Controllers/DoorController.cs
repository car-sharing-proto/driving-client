using UnityEngine;

namespace Core.Car
{
    public class DoorController : Controller
    {
        [SerializeField] private Door[] _doors;
        public override bool Check()
        {
            foreach (Door door in _doors)
            {
                if(door.State != IOpenable.OpenState.CLOSED) 
                {
                    return true;
                }
            }

            return false;
        }
    }
}
