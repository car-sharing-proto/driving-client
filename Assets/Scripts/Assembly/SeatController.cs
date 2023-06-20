using UnityEngine;

[RequireComponent(typeof(Core.Character.SeatPlace))]
[RequireComponent(typeof(Core.Car.Seat))]
public class SeatController : MonoBehaviour
{
    [SerializeField] private Core.Car.Door _door;
    private Core.Character.SeatPlace _playerSeat;
    private Core.Car.Seat _carSeat;

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
    }
}
