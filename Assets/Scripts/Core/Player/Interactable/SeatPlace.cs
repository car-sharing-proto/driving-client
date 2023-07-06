using UnityEngine;

namespace Core.Player
{
    public class SeatPlace : MonoBehaviour
    {
        [SerializeField] private Transform _placePoint;
        [SerializeField] private Transform _leavePoint;

        private ISitting _sitting = null;

        public bool IsTaken => _sitting != null;
        public bool IsLocked { get; set; } = false;
        public ISitting Sitting => _sitting;

        private void Update()
        {
            CheckLeaving();
        }

        public bool IsInteractable(ISitting sitable)
        {
            return !IsLocked && !IsTaken && !sitable.IsSitting;
        }

        public bool Take(ISitting sitable)
        {
            if (!IsInteractable(sitable))
            {
                return false;
            }

            _sitting = sitable;
            _sitting.SitDown(_placePoint);

            return true;
        }

        public void Free()
        {
            if (IsLocked)
            {
                _sitting.SitDown(_placePoint);
                return;
            }

            _sitting.StandUp(_leavePoint);
            _sitting = null;
        }

        private void CheckLeaving()
        {
            if (_sitting == null || _sitting.IsSitting)
            {
                return;
            }

            Free();
        }
    }
}