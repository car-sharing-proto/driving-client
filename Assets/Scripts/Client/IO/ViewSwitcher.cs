using System;
using UnityEngine;
using Core.Cameras;

[Serializable]
public class ViewSwitcher
{
    [SerializeField] private MovableCamera[] _cameras;
    [SerializeField] private int _mainCameraIndex;

    private UserController _userController;
    private int _currentCameraIndex;

    public void Initialize(UserController userController)
    {
        _userController = userController;
    }

    public void Switch()
    {
        _currentCameraIndex++;

        if (_currentCameraIndex >= _cameras.Length)
        {
            _currentCameraIndex = 0;
        }
    }

    public void Update()
    {
        if(!_userController.PlayerController.PlayerBody.IsSitting)
        {
            _currentCameraIndex = _mainCameraIndex;
        }

        for (int i = 0; i < _cameras.Length; i++)
        {
            _cameras[i].SetActive(_currentCameraIndex == i);
        }
    }
}
