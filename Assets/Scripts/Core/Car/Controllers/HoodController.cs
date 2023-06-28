using UnityEngine;

namespace Core.Car
{
    public class HoodController : Controller
    {
        [SerializeField] private Hood _hood;
        public override bool Check()
        {
            return _hood.State != IOpenable.OpenState.CLOSED;
        }
    }
}