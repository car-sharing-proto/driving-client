using UnityEngine;

namespace Core.Car
{
    public class StaticLights : MonoBehaviour
    {
        [SerializeField] private Car _car;
        [SerializeField] private LightFixture[] _lights;
        
        private void Update()
        {
            for(var i = 0; i < _lights.Length; i++)
            {
                _lights[i].SetLight(_car.Engine.Enabled);
            }
        }
    }
}
