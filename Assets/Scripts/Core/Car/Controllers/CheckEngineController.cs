using UnityEngine;

namespace Core.Car
{
    public class CheckEngineController : Controller
    {
        [SerializeField] private Car _car;

        public override bool Check()
        {
            return _car.Engine.Starter.IsStarting;
        }
    }
}