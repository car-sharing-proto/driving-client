using System;
using UnityEngine;

using OpenState = Core.Car.IOpenable.OpenState;

namespace Core.Car
{
    [RequireComponent(typeof(MeshCollider))]
    public class Door : MonoBehaviour, IOpenable
    {
        [SerializeField] private Vector3 _startAngle;
        [SerializeField] private Vector3 _endAngle;
        [SerializeField] private float _openSpeed;

        protected MeshCollider _collider;

        private Animation.Vector3_LinearAnimation _openAnimation;
        private Animation.Vector3_LinearAnimation _closeAnimation;

        public Vector3 StartAngle => _startAngle;
        public Vector3 EndAngle => _endAngle;
        public OpenState State { get; private set; }

        public bool IsInteractable => State is OpenState.OPEN or OpenState.CLOSED;

        public Action<OpenState> OnStateChange;

        private void Awake()
        {
            State = OpenState.CLOSED;

            _collider = GetComponent<MeshCollider>();

            _openAnimation = new(StartAngle, EndAngle, _openSpeed,
                angles => transform.localEulerAngles = angles,
                () => SetState(OpenState.OPEN));
            _closeAnimation = new(EndAngle, StartAngle, _openSpeed,
                angles => transform.localEulerAngles = angles,
                () => SetState(OpenState.CLOSED));
        }

        private void Update()
        {
            _collider.enabled = IsInteractable;
        }

        public void Open()
        {
            StartCoroutine(_openAnimation.GetAnimationCoroutine());

            SetState(OpenState.IS_OPENING);
        }

        public void Close()
        {
            StartCoroutine(_closeAnimation.GetAnimationCoroutine());

            SetState(OpenState.IS_CLOSING);
        }

        public void Interact()
        {
            switch (State)
            {
                case OpenState.OPEN:
                    Close();
                    break;
                case OpenState.CLOSED:
                    Open();
                    break;
                default:
                    break;
            }
        }

        private void SetState(OpenState state)
        {
            if(State == state)
            {
                return;
            }

            State = state;

            OnStateChange?.Invoke(State);
        }
    }
}