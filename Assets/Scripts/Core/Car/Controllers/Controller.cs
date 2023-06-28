using UnityEngine;

namespace Core.Car
{
    public abstract class Controller : MonoBehaviour
    {
        [SerializeField] private SignalLamp _signalLamp;

        public abstract bool Check();

        private void Update()
        {
            if (_signalLamp == null)
            {
                return;
            }

            _signalLamp.SetActive(Check());
        }
    }
}