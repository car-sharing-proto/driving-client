using System.Collections.Generic;
using UnityEngine;

namespace Core.Car
{
    [System.Serializable]
    public class LightGroup
    {
        [SerializeField] private List<LightFixture> _lights = new();

        public void SetLight(bool state)
        {
            for (int i = 0; i < _lights.Count; i++)
            {
                _lights[i].SetLight(state);
            }
        }
    }
}