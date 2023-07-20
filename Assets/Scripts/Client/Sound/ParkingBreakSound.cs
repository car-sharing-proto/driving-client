
using Core.Car;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ParkingBreakSound
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _parkingBreakOn;
    [SerializeField] private AudioClip _parkingBreakOff;

    private ParkingBreak _parkingBreak;

    public void Initialize(ParkingBreak parkingBreak)
    {
        _parkingBreak = parkingBreak;

        _parkingBreak.OnBreakSwitch += PlayParkingBreakSound;
    }

    public void Destroy()
    {
        _parkingBreak.OnBreakSwitch -= PlayParkingBreakSound;
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
