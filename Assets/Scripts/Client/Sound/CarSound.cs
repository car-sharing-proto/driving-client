using Core.Car;
using UnityEngine;

public class CarSound : MonoBehaviour
{
    [SerializeField] private Car _car;

    [SerializeField] private EngineSound _engineSound;
    [SerializeField] private BlinkerSound _blinkerSound;
    [SerializeField] private TransmissionSound _transmissionSound;
    [SerializeField] private ParkingBreakSound _parkingBreakSound;
    [SerializeField] private DoorSound _doorSound;

    private void Awake()
    {
        _engineSound.Initialize(_car.Engine);
        _blinkerSound.Initialize(_car.TurnLights);
        _transmissionSound.Initialize(_car.Transmission);
        _parkingBreakSound.Initialize(_car.ParkingBreak);
        _doorSound.Initialize();
    }

    private void OnDestroy()
    {
        _engineSound.Destroy();
        _blinkerSound.Destroy();
        _transmissionSound.Destroy();
        _parkingBreakSound.Destroy();
        _doorSound.Destroy();
    }

    private void Update()
    {
        _engineSound.Update();
    }
}
