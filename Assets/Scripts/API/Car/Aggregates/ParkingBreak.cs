using UnityEngine;
using Core.Animation;

namespace Core.Car
{
    public enum ParkingBreakState
    {
        RAISED,
        LOWERED,
        SWITCHING
    }

    public class ParkingBreak : MonoBehaviour
    {
        [SerializeField] private Vector3 _startAngle;
        [SerializeField] private Vector3 _endAngle;
        [SerializeField] private float _openSpeed;

        private Vector3_LinearAnimation _lowerAnimation;
        private Vector3_LinearAnimation _raiseAnimation;

        public float Break { get; private set; }
        public Vector3 StartAngle => _startAngle;
        public Vector3 EndAngle => _endAngle;
        public ParkingBreakState State { get; private set; }

        private void Start()
        {
            State = ParkingBreakState.RAISED;

            _lowerAnimation = new(StartAngle, EndAngle, _openSpeed,
                angles => transform.localEulerAngles = angles,
                () => State = ParkingBreakState.LOWERED);
            _raiseAnimation = new(EndAngle, StartAngle, _openSpeed,
                angles => transform.localEulerAngles = angles,
                () => State = ParkingBreakState.RAISED);
        }

        private void Update()
        {
            Break = State == ParkingBreakState.LOWERED ? 0.0f : 1.0f;
        }

        public void Switch()
        {
            switch (State)
            {
                case ParkingBreakState.LOWERED:
                    Raise();
                    break;
                case ParkingBreakState.RAISED:
                    Lower();
                    break;
                default:
                    break;
            }
        }

        private void Lower()
        {
            if (State == ParkingBreakState.SWITCHING)
            {
                return;
            }

            State = ParkingBreakState.SWITCHING;

            StartCoroutine(_lowerAnimation.GetAnimationCoroutine());
        }

        private void Raise()
        {
            if (State == ParkingBreakState.SWITCHING)
            {
                return;
            }

            State = ParkingBreakState.SWITCHING;

            StartCoroutine(_raiseAnimation.GetAnimationCoroutine());
        }
    }
}