using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(MeshRenderer))]
public class LightFixture : MonoBehaviour
{
    [SerializeField, ColorUsage(true, true)] protected Color _color;
    [SerializeField] protected float _minLight;
    [SerializeField] protected float _maxLight;
    [SerializeField] protected int _index;
    [SerializeField] protected float _speed = 4.1f;

    private MeshRenderer _renderer;
    protected bool _state = false;
    protected float _transition = 0f;

    public void SetLight(bool state) 
    {
        this._state = state;
    }

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    protected void FixedUpdate()
    {
        UpdateTransition();
        UpdateLight();
    }

    protected void UpdateTransition() 
    {
        if (_state)
        {
            if (_transition < 1f)
            {
                _transition += _speed * Time.deltaTime;
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
                _transition -= _speed * Time.deltaTime;
            }
            else
            {
                _transition = 0f;
            }
        }
    }

    protected virtual void UpdateLight()
    {
        float t = Mathf.Lerp(_minLight, _maxLight, _transition);
        float factor = Mathf.Pow(2, (t + 1));

        _renderer.materials[_index].SetColor("_EmissionColor", _color * factor);
    }
}
