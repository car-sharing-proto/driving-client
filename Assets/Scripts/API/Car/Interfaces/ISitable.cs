using UnityEngine;

namespace Core.Car
{
    public interface ISitable
    {
        public bool IsSitting { get; }
        public void SitDown(Transform placePoint);
        public void StandUp(Transform leavePoint);
    }
}