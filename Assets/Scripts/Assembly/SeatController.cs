using UnityEngine;

[RequireComponent(typeof(Core.Character.SeatPlace))]
[RequireComponent(typeof(Core.Car.Seat))]
public class SeatController : MonoBehaviour
{
    [SerializeField] private Core.Car.Door _door;
    private Core.Character.SeatPlace _playerSeat;
    private Core.Car.Seat _carSeat;
    private UserCharacterController _characterController;

    private void Start()
    {
        _playerSeat = GetComponent<Core.Character.SeatPlace>();
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

    public bool IsInteractable(UserCharacterController characterController)
    {
        return _playerSeat.IsInteractable(characterController.CharacterBody);
    }

    public void Take(UserCharacterController characterController)
    {
        if (_playerSeat.Take(characterController.CharacterBody))
        {
            _characterController = characterController;

            if (_carSeat.IsDriverSeat)
            {
                _characterController.CarController =
                    new Core.Car.CarController(_carSeat.Car);
            }
        }
    }
}
