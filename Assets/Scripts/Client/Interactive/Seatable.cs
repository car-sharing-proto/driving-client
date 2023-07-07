using System;
using UnityEngine;

[RequireComponent(typeof(Core.Player.SeatPlace))]
[RequireComponent(typeof(Core.Car.Seat))]
public class Seatable : MonoBehaviour, IInteractive
{
    [SerializeField] private Core.Car.Door _door;
    [SerializeField] private string _hintText;

    private Core.Player.SeatPlace _playerSeat;
    private Core.Car.Seat _carSeat;
    private UserController _userControler;

    public Action<UserController> OnUserSitting;
    public Action<UserController> OnUserLeaving;

    public string Hint => _hintText;

    private void Awake()
    {
        _playerSeat = GetComponent<Core.Player.SeatPlace>();
        _carSeat = GetComponent<Core.Car.Seat>();

        _playerSeat.OnSitting += Refresh;
        _playerSeat.OnLeaving += Refresh;
    }

    private void OnDestroy()
    {
        _playerSeat.OnSitting -= Refresh;
        _playerSeat.OnLeaving -= Refresh;
    }

    private void Update()
    {
        _playerSeat.IsLocked = _door.State
            == Core.Car.IOpenable.OpenState.CLOSED;
    }

    private void Refresh()
    {
        _carSeat.IsTaken = _playerSeat.IsTaken;

        if (_userControler is not null && !_playerSeat.IsTaken)
        {
            OnUserLeaving?.Invoke(_userControler);

            _userControler = null;
        }
    }

    public bool IsInteractable(UserController user)
    {
        return _playerSeat.IsInteractable(
            user.PlayerController.PlayerBody);
    }

    public void Interact(UserController userController)
    {
        if (_playerSeat.Take(userController.
            PlayerController.PlayerBody))
        {
            _userControler = userController;

            OnUserSitting?.Invoke(_userControler);
        }
    }
}
