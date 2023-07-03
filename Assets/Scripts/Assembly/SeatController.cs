using UnityEngine;

[RequireComponent(typeof(Core.Player.SeatPlace))]
[RequireComponent(typeof(Core.Car.Seat))]
public class SeatController : MonoBehaviour
{
    [SerializeField] private Core.Car.Door _door;
    private Core.Player.SeatPlace _playerSeat;
    private Core.Car.Seat _carSeat;
    private UserController _userControler;

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

        if (_userControler is not null && !_playerSeat.IsTaken)
        {
            _userControler.CarController.SetCar(null);

            _userControler = null;
        }
    }

    public bool IsInteractable(UserController user)
    {
        return _playerSeat.IsInteractable(user.PlayerController.PlayerBody);
    }

    public void Take(UserController user)
    {
        if (_playerSeat.Take(user.PlayerController.PlayerBody))
        {
            _userControler = user;

            _userControler.CarController.SetCar(_carSeat.ProvideControllableCar());
        }
    }
}
