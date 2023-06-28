using UnityEngine;

namespace Core.Car
{
    public class HighLightController : Controller
    {
        [SerializeField] private Car _car;

        public override bool Check()
        {
            return _car.HeadLights.State == HeadLightState.HIGH;
        }
    }
}
