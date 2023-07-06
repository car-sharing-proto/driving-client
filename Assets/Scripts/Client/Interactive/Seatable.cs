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

    public string Hint => _hintText;

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
        return _playerSeat.IsInteractable(
            user.PlayerController.PlayerBody);
    }

    public void Interact(UserController userController)
    {
        if (_playerSeat.Take(userController.
            PlayerController.PlayerBody))
        {
            _userControler = userController;

            _userControler.CarController.SetCar(
                _carSeat.ProvideControllableCar());
        }
    }
}
