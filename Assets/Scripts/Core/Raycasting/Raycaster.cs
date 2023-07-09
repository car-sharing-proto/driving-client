using UnityEngine;

namespace Core.Raycasting
{
    public class Raycaster
    {
        private readonly Transform _target;
        private readonly float _rayLength;

        public Raycaster(Transform target, float rayLength)
        {
            this._target = target;
            this._rayLength = rayLength;
        }

        public T CheckHit<T>() where T : class
        {
            if (Physics.Raycast(_target.position,
                _target.forward, out RaycastHit hit, _rayLength))
            {
                var tempMonoArray = hit.collider.
                    gameObject.GetComponents<MonoBehaviour>();

                foreach (var monoBehaviour in tempMonoArray)
                {
                    if (monoBehaviour is T)
                    {
                        return monoBehaviour as T;
                    }
                }
            }

            return null;
        }
    }
}