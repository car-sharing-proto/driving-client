using UnityEngine;

[RequireComponent(typeof(Core.Player.SeatPlace))]
[RequireComponent(typeof(Core.Car.Seat))]
public class SeatController : MonoBehaviour
{
    [SerializeField] private Core.Car.Door _door;
    private Core.Player.SeatPlace _playerSeat;
    private Core.Car.Seat _carSeat;
    private User _characterController;

    private void Awake()
    {
        _playerSeat = GetComponent<Core.Player.SeatPlace>();
        _carSeat = GetComponent<Core.Car.Seat>();
    }

    private void Update()
    {
        _carSeat.IsTaken = _playerSeat.IsTaken;
        _playerSeat.IsLocked = 
            _door.State != Core.Car.IOpenable.OpenState.OPEN;

        if (_characterController != null &&
            _carSeat.IsDriverSeat && !_playerSeat.IsTaken)
        {
            _characterController.CarController = null;
            _characterController = null;
        }
    }

    public bool IsInteractable(User user)
    {
        return _playerSeat.IsInteractable(user.PlayerController.PlayerBody);
    }

    public void Take(User user)
    {
        if (_playerSeat.Take(user.PlayerController.PlayerBody))
        {
            _characterController = user;

            if (_carSeat.IsDriverSeat)
            {
                // TODO: Remove this cringe.
                _characterController.CarController =
                    _carSeat.CarController;
            }
        }
    }
}
