using UnityEngine;

namespace Core.Cameras
{
    public class FPCamera : MovableCamera
    {
        [SerializeField] private float _sensitivity = 10;
        [SerializeField] private float _maxFocalLength = 60;
        [SerializeField] private float _minFocalLength = 15;

        private float focalLength = 50;

        private void Update()
        {
            if (!IsMovable)
            {
                return;
            }

            var dz = Input.mouseScrollDelta.y;

            focalLength -= dz * _sensitivity;

            if (focalLength > _maxFocalLength)
            {
                focalLength = _maxFocalLength;
            }
            if (focalLength < _minFocalLength)
            {
                focalLength = _minFocalLength;
            }

            _camera.fieldOfView = focalLength;
        }

    }
}