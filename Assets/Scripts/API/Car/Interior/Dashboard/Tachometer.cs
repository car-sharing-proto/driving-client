using UnityEngine;

namespace Core.Car
{
    public class Tachometer : MonoBehaviour
    {
        [SerializeField] private Transform _needle;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _maxRPM;
        
        public void UpdateValue(float rpm)
        {
            var ratio = rpm / _maxRPM;
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