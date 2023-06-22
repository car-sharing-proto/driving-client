using UnityEngine;

namespace Core.Car
{
    public class Tachometer : MonoBehaviour
    {
        [SerializeField] private Transform _needle;
        [SerializeField] private float _maxAngle;

        private Engine _engine;

        public void SetEngine(Engine engine)
        {
            this._engine = engine;
        }
        
        private void Update()
        {
            var ratio = _engine.RPM / _engine.MaxRPM;
            if(ratio > 1)
            {
                ratio = 1;
            }

            if (ratio < 0)
            {
                ratio = 0;
            }

            _needle.localEulerAngles = 
                _maxAngle * ratio * Vector3.right;
        }
    }
}