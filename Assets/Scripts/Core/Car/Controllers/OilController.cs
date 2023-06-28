using UnityEngine;

namespace Core.Car
{
    public class OilController : Controller
    {
        [SerializeField] private Car _car;

        public override bool Check()
        {
            return _car.Engine.Starter.IsStarting;
        }
    }
}