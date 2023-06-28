using UnityEngine;

namespace Core.Player
{
    public class SeatPlace : MonoBehaviour
    {
        [SerializeField] private Transform _placePoint;
        [SerializeField] private Transform _leavePoint;

        private ISitable _sitable = null;

        public bool IsTaken => _sitable != null;
        public bool IsLocked { get; set; } = false;
        public ISitable Sitable => _sitable;

        private void Update()
        {
            CheckLeaving();
        }

        public bool IsInteractable(ISitable sitable)
        {
            return !IsLocked && !IsTaken && !sitable.IsSitting;
        }

        public bool Take(ISitable sitable)
        {
            if (!IsInteractable(sitable))
            {
                return false;
            }

            _sitable = sitable;
            _sitable.SitDown(_placePoint);

            return true;
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
            {
                return;
            }

            Free();
        }
    }
}