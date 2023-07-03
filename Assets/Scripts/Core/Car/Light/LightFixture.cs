using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Car
{
    [System.Serializable]
    [RequireComponent(typeof(MeshRenderer))]
    public class LightFixture : MonoBehaviour
    {
        [SerializeField, ColorUsage(true, true)] private Color _color;
        [SerializeField] private float _minLight;
        [SerializeField] private float _maxLight;
        [SerializeField] private int _index;
        [SerializeField] private float _flashSpeed = 5f;
        [SerializeField] private float _fadeSpeed = 5f;

        private MeshRenderer _renderer;
        private bool _state = false;
        private float _transition = 0f;

        public void SetLight(bool state)
        {
            this._state = state;
        }

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        private void FixedUpdate()
        {
            UpdateTransition();
            UpdateLight();
        }

        private void UpdateTransition()
        {
            if (_state)
            {
                if (_transition < 1f)
                {
                    _transition += _flashSpeed * Time.deltaTime;
                }
                else
                {
                    _transition = 1f;
                }
            }
            else
            {
                if (_transition > 0f)
                {
                    _transition -= _fadeSpeed * Time.deltaTime;
                }
                else
                {
                    _transition = 0f;
                }
            }
        }

        private void UpdateLight()
        {
            var t = Mathf.Lerp(_minLight, _maxLight, _transition);
            var factor = Mathf.Pow(2, (t + 1));

            _renderer.materials[_index].SetColor("_EmissionColor", _color * factor);
        }
    }
}