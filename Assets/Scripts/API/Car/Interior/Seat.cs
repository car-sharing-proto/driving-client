using UnityEngine;

namespace Core.Car
{
    public class Seat : MonoBehaviour
    {
        [SerializeField] private Door _door;
        [SerializeField] private Transform _placePoint;
        [SerializeField] private Transform _leavePoint;

        private ISitable _sitable = null;

        public bool IsTaken => _sitable != null;

        private void Update()
        {
            CheckLeaving();
        }

        public void Take(ISitable sitable)
        {
            if (_door.State != IOpenable.OpenState.OPEN ||IsTaken || sitable.IsSitting)
            {
                return;
            }

            _sitable = sitable;
            _sitable.SitDown(_placePoint);
        }

        public void Free()
        {
            if (_door.State != IOpenable.OpenState.OPEN)
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
