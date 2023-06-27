using UnityEngine;

namespace Core.Car
{
    public class DippedLightController : Controller
    {
        [SerializeField] private Car _car;

        public override bool Check()
        {
            return _car.Engine.Starter.State == EngineState.STARTED;
        }
    }
}
