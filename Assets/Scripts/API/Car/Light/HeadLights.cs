using System;
using System.Linq;
using UnityEngine;

namespace Core.Car
{
    public enum HeadLightState : int
    {
        DIPPED = 0,
        HIGH = 1
    }

    [System.Serializable]
    public class HeadLights
    {
        [SerializeField] private LightFixture _highLight;

        public HeadLightState State { get; set; }

        public HeadLights()
        {
            State = HeadLightState.DIPPED;
        }

        public void Update()
        {
            _highLight.SetLight(State == HeadLightState.HIGH);
        }

        public void Switch()
        {
            var state = (int)State;
            var lastState = Enum.GetValues(typeof(HeadLightState)).
                Cast<int>().Last();

            State = (HeadLightState)((state + 1) % (lastState + 1));
        }
    }
}
