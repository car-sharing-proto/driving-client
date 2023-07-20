using System;
using Core.Car;
using UnityEngine;

[Serializable]
public class TransmissionSound
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _modeShift;

    private Transmission _transmission;

    public void Initialize(Transmission transmission)
    {
        _transmission = transmission;

        _transmission.OnModeChange += PlayTransmissionModeSound;
    }

    public void Destroy()
    {
        _transmission.OnModeChange -= PlayTransmissionModeSound;
    }

    private void PlayTransmissionModeSound(TransmissionMode mode)
    {
        _audioSource.PlayOneShot(_modeShift, 2.0f);
    }
}
