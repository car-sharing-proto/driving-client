using UnityEngine;

namespace Core.Car
{
    [System.Serializable]
    public class TurnLights
    {
        [System.Flags]
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
        private float _turnLightsphasa = 0;

        public State LightState { get; private set; }

        public void Update()
        {
            _turnLightsphasa += _turnLightsSpeed * Time.deltaTime;

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
            lightGroup.SetLight((int)_turnLightsphasa % 2 == 0);
        }
    }
}