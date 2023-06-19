using UnityEngine;

[RequireComponent(typeof(Core.Character.Seat))]
[RequireComponent(typeof(Core.Car.Seat))]
public class SeatController : MonoBehaviour
{
    [SerializeField] private Core.Car.Door _door;
    private Core.Character.Seat _playerSeat;
    private Core.Car.Seat _carSeat;

    private void Start()
    {
        _playerSeat = GetComponent<Core.Character.Seat>();
        _carSeat = GetComponent<Core.Car.Seat>();
    }

    private void Update()
    {
        _carSeat.IsTaken = _playerSeat.IsTaken;
        _playerSeat.IsLocked = 
            _door.State != Core.Car.IOpenable.OpenState.OPEN;
    }
}
