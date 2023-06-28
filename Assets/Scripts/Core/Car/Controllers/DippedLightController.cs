using UnityEngine;

namespace Core.Car
{
    public class DippedLightController : Controller
    {
        [SerializeField] private Car _car;

        public override bool Check()
        {
            return _car.Engine.Enabled
                && _car.HeadLights.State == HeadLightState.DIPPED;
        }
    }
}
