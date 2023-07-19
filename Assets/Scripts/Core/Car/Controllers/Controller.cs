using System;
using UnityEngine;

namespace Core.Car
{
    public abstract class Controller : MonoBehaviour
    {
        [SerializeField] private SignalLamp _signalLamp;

        private bool _state = false;

        public Action<bool> OnStateChange;
        public bool State => _state;

        public abstract bool Check();

        private void Update()
        {
            if (_signalLamp == null)
            {
                return;
            }

            var check = Check();

            if(check != _state)
            {
                _state = check;

                OnStateChange?.Invoke(check);
            }

            _signalLamp.SetActive(check);
        }
    }
}