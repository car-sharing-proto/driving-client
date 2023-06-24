using UnityEngine;

namespace Core.Car
{
    public enum EngineState
    {
        STOPED,
        STARTED
    }

    [System.Serializable]
    public class Starter
    {
        private const float c_startSpeed = 0.4f;
        [SerializeField] private AnimationCurve _startEngine;
        [SerializeField] private AnimationCurve _stopEngine;
        private float _runningValue;
        private float _runningTransition;

        public EngineState State { get; private set; } = EngineState.STOPED;
        public float Value => _runningValue;

        public void SetState(EngineState state)
        {
            if (_runningTransition > 0.0f &&
                _runningTransition < 1.0f)
            {
                return;
            }

            State = state;
        }

        public void Update()
        {
            if (State == EngineState.STARTED && _runningTransition < 1.0f)
            {
                _runningTransition += c_startSpeed * Time.deltaTime;
                _runningValue = _startEngine.Evaluate(_runningTransition);
            }
            if (State == EngineState.STOPED && _runningValue > 0.0f)
            {
                _runningTransition -= c_startSpeed * Time.deltaTime;
                _runningValue = _stopEngine.Evaluate(_runningTransition);
            }

            _runningTransition = Mathf.Clamp(_runningTransition, 0.0f, 1.0f);
        }
    }
}