using System;
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

        public Action OnSitting { get; set; }
        public Action OnLeaving { get; set; }

        private void Update()
        {
            CheckLeaving();
        }

        public bool IsInteractable(ISitting sitting)
        {
            return !IsLocked && !IsTaken && !sitting.IsSitting;
        }

        public bool Take(ISitting sitting)
        {
            if (!IsInteractable(sitting))
            {
                return false;
            }

            _sitting = sitting;
            _sitting.SitDown(_placePoint);

            OnSitting?.Invoke();

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

            OnLeaving?.Invoke();
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