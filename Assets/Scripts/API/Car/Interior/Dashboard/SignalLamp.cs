using UnityEngine;

namespace Core.Car
{
    [RequireComponent(typeof(LightFixture))]
    public class SignalLamp : MonoBehaviour
    {
        private LightFixture _light;

        public bool IsActive {  get; private set; }

        private void Awake()
        {
            _light = GetComponent<LightFixture>();
        }

        public void SetActive (bool active)
        {
            if(IsActive == active)
            {
                return;
            }

            IsActive = active;

            _light.SetLight(IsActive);
        }
    }
}