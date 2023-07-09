using UnityEngine;

namespace Core.Car
{
    public class RotaryMultiselector : MonoBehaviour, IInteractive
    {
        [SerializeField] private Vector3[] _positionAngles;
        [SerializeField] private Transform _handle;

        private int _positionAngleIndex;

        public bool IsInteractable => true;

        private void Awake()
        {
            _positionAngleIndex = 0;

            SetPositionAngleByIndex(_positionAngleIndex);
        }

        public void Interact()
        {
            _positionAngleIndex++;

            if(_positionAngleIndex >= _positionAngles.Length)
            {
                _positionAngleIndex = 0;
            }

            SetPositionAngleByIndex(_positionAngleIndex);
        }

        private void SetPositionAngleByIndex(int index)
        {
            _handle.transform.localEulerAngles = _positionAngles[index];
        }
    }
}