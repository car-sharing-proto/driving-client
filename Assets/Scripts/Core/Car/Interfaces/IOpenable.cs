using UnityEngine;

namespace Core.Car
{
    public interface IOpenable : IInteractive
    {
        public enum OpenState
        {
            OPEN,
            CLOSED,
            IS_OPENING,
            IS_CLOSING
        }

        public Vector3 StartAngle { get; }
        public Vector3 EndAngle { get; }
        public OpenState State { get; }
        public void Open();
        public void Close();
    }
}