using UnityEngine;

namespace Core.Character
{
    public class Seat : MonoBehaviour
    {
        [SerializeField] private Transform _placePoint;
        [SerializeField] private Transform _leavePoint;

        private ISitable _sitable = null;

        public bool IsTaken => _sitable != null;
        public bool IsLocked { get; set; } = false;

        private void Update()
        {
            CheckLeaving();
        }

        public bool IsInteractable(ISitable sitable)
        {
            return !IsLocked && !IsTaken && !sitable.IsSitting;
        }

        public void Take(ISitable sitable)
        {
            if (!IsInteractable(sitable))
            {
                return;
            }

            _sitable = sitable;
            _sitable.SitDown(_placePoint);
        }

        public void Free()
        {
            if (IsLocked)
            {
                _sitable.SitDown(_placePoint);
                return;
            }

            _sitable.StandUp(_leavePoint);
            _sitable = null;
        }

        private void CheckLeaving()
        {
            if (_sitable == null || _sitable.IsSitting)
                return;

            Free();
        }
    }
}