using UnityEngine;

namespace Core.Car
{
    public class TransmissionSelector : MonoBehaviour
    {
        [SerializeField] private Car _car;
     
        [SerializeField] private LightFixture _p;
        [SerializeField] private LightFixture _r;
        [SerializeField] private LightFixture _n;
        [SerializeField] private LightFixture _d;

        private void Update()
        {
            var enabled = _car.Engine.Enabled;
            var mode = _car.Transmission.Mode;

            _p.SetLight(mode == TransmissionMode.PARKING && enabled);
            _r.SetLight(mode == TransmissionMode.REVERSE && enabled);
            _n.SetLight(mode == TransmissionMode.NEUTRAL && enabled);
            _d.SetLight(mode == TransmissionMode.DRIVING && enabled);
        }
    }
}