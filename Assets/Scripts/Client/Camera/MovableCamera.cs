using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MovableCamera : MonoBehaviour
{
    [SerializeField] private bool _isMovable = true;

    protected Camera _camera;
    public bool IsMovable => _isMovable;
    public void SetMovable(bool state) 
    {
        _isMovable = state;
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
}
