using UnityEngine;

using OpenState = Core.Car.IOpenable.OpenState;

namespace Core.Car
{
    [RequireComponent(typeof(MeshCollider))]
    public class Door : MonoBehaviour, IOpenable, IInteractive
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

        private void Awake()
        {
            State = OpenState.CLOSED;

            _collider = GetComponent<MeshCollider>();

            _openAnimation = new(StartAngle, EndAngle, _openSpeed,
                angles => transform.localEulerAngles = angles,
                () => State = OpenState.OPEN);
            _closeAnimation = new(EndAngle, StartAngle, _openSpeed,
                angles => transform.localEulerAngles = angles,
                () => State = OpenState.CLOSED);
        }

        private void Update()
        {
            _collider.enabled = IsInteractable;
        }

        public void Open()
        {
            StartCoroutine(_openAnimation.GetAnimationCoroutine());

            State = OpenState.IS_OPENING;
        }

        public void Close()
        {
            StartCoroutine(_closeAnimation.GetAnimationCoroutine());

            State = OpenState.IS_CLOSING;
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
    }
}