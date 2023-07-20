using Core.Car;
using UnityEngine;
using System.Linq;

public class CarSound : MonoBehaviour
{
    [SerializeField] private EngineSound _engineSound;
    [SerializeField] private BlinkerSound _blinkerSound;
    [SerializeField] private TransmissionSound _transmissionSound;

    [SerializeField] private AudioSource _doorSystemAudioSource;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _parkingBreakOn;
    [SerializeField] private AudioClip _parkingBreakOff;
    [SerializeField] private AudioClip _openDoor;
    [SerializeField] private AudioClip _closeDoor;
    [SerializeField] private AudioClip _slamLid;
    [SerializeField] private AudioClip _gaslift;

    [SerializeField] private Car _car;

    [SerializeField] private Door[] _doors;
    [SerializeField] private Door _hood;
    [SerializeField] private Door _trunk;

    [SerializeField] private Controller[] _openControllers;


    private void Awake()
    {
        _engineSound.Initialize(_car.Engine);
        _blinkerSound.Initialize(_car.TurnLights);
        _transmissionSound.Initialize(_car.Transmission);

        _car.ParkingBreak.OnBreakSwitch += PlayParkingBreakSound;
       
        _hood.OnStateChange += PlayLidSound;
        _trunk.OnStateChange += PlayLidSound;

        for (int i = 0; i < _doors.Length; i++)
        {
            _doors[i].OnStateChange += PlayDoorSound;
        }

        for (int i = 0; i < _openControllers.Length; i++)
        {
            _openControllers[i].OnStateChange += PlayOpenDoorSound;
        }
    }

    private void OnDestroy()
    {
        _engineSound.Destroy();
        _blinkerSound.Destroy();
        _transmissionSound.Destroy();

        _car.ParkingBreak.OnBreakSwitch -= PlayParkingBreakSound;
        _hood.OnStateChange -= PlayLidSound;
        _trunk.OnStateChange -= PlayLidSound;

        for (int i = 0; i < _doors.Length; i++)
        {
            _doors[i].OnStateChange -= PlayDoorSound;
        }

        for (int i = 0; i < _openControllers.Length; i++)
        {
            _openControllers[i].OnStateChange -= PlayOpenDoorSound;
        }
    }

    private void Update()
    {
        _engineSound.Update();
    }





    private void PlayDoorSound(IOpenable.OpenState state)
    {
        switch (state)
        {
            case IOpenable.OpenState.CLOSED:
                _audioSource.PlayOneShot(_closeDoor);
                break;
            case IOpenable.OpenState.IS_OPENING:
                _audioSource.PlayOneShot(_openDoor);
                break;
            default:
                break;
        }
    }

    private void PlayLidSound(IOpenable.OpenState state)
    {
        switch (state)
        {
            case IOpenable.OpenState.IS_CLOSING:
                _audioSource.PlayOneShot(_gaslift);
                break;
            case IOpenable.OpenState.CLOSED:
                _audioSource.PlayOneShot(_slamLid);
                break;
            case IOpenable.OpenState.IS_OPENING:
                _audioSource.PlayOneShot(_openDoor);
                _audioSource.PlayOneShot(_gaslift);
                break;
            default:
                break;
        }
    }

    private void PlayOpenDoorSound(bool state)
    {
        if(state == _doorSystemAudioSource.isPlaying)
        {
            return;
        }

        var controllers = from controller in _openControllers
                          where controller.State
                          select controller;

        if (controllers.Count() > 0)
        {
            _doorSystemAudioSource.Play();
        }
        else
        {
            _doorSystemAudioSource.Stop();
        }
    }



    private void PlayParkingBreakSound(ParkingBreakState state)
    {
        switch (state)
        {
            case ParkingBreakState.SWITCHING_UP:
                _audioSource.PlayOneShot(_parkingBreakOn);
                break;
            case ParkingBreakState.SWITCHING_DOWN:
                _audioSource.PlayOneShot(_parkingBreakOff);
                break;
            default:
                break;
        }
    }
}
