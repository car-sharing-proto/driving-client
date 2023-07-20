using System;
using UnityEngine;

namespace Core.Car
{
    [Serializable]
    public class TurnLights
    {
        [Flags]
        public enum State
        {
            NONE = 0,
            LEFT = 1,
            RIGHT = 2,
            BOTH = 4
        }

        [SerializeField] private LightGroup _leftLights;
        [SerializeField] private LightGroup _rightLights;

        private readonly float _turnLightsSpeed = 2.3f;
        private float _turnLightsPhasa = 0;
        private bool _blinkState = false;

        public State LightState { get; private set; }

        public Action<bool> OnBlinkerSwitch;

        public void Update()
        {
            if(LightState == State.NONE) 
            {
                ClearPhasa();
            }
            else
            {
                UpdatePhasa();
                SetBlinkState((int)_turnLightsPhasa % 2 == 0);
            }

            ControlFlashing(_rightLights, State.RIGHT);
            ControlFlashing(_leftLights, State.LEFT);
        }

        public void SwitchLeft()
        {
            if (State.RIGHT == (State.RIGHT & LightState))
            {
                LightState ^= State.RIGHT;
            }

            LightState ^= State.LEFT;
        }

        public void SwitchRight()
        {
            if (State.LEFT == (State.LEFT & LightState))
            {
                LightState ^= State.LEFT;
            }

            LightState ^= State.RIGHT;
        }

        public void SwitchEmergency()
        {
            LightState ^= State.BOTH;
        }

        private void ControlFlashing(LightGroup lightGroup, State state)
        {
            if ((LightState & state) == state ||
                (LightState & State.BOTH) == State.BOTH)
            {
                Flashing(lightGroup);
            }
            else
            {
                lightGroup.SetLight(false);
            }
        }

        private void Flashing(LightGroup lightGroup)
        {
            lightGroup.SetLight(_blinkState);
        }

        private void UpdatePhasa()
        {
            _turnLightsPhasa += _turnLightsSpeed * Time.deltaTime;
        }

        private void ClearPhasa()
        {
            _blinkState = false;
            _turnLightsPhasa = 0;
        }

        private void SetBlinkState(bool state)
        {
            if(state == _blinkState)
            {
                return;
            }
            
            _blinkState = state;

            OnBlinkerSwitch?.Invoke(state);
        }

    }
}