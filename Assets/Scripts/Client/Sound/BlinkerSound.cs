using System;
using Core.Car;
using UnityEngine;

[Serializable]
public class BlinkerSound
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _blinkerOn;
    [SerializeField] private AudioClip _blinkerOff;

    [SerializeField] private float _blinkerVolumeMultiplier = 2.0f;

    private TurnLights _turnLights;

    public void Initialize(TurnLights turnLights)
    {
        _turnLights = turnLights;

        _turnLights.OnBlinkerSwitch += PlayBlinkerSound;
    }

    public void Destroy()
    {
        _turnLights.OnBlinkerSwitch -= PlayBlinkerSound;
    }

    private void PlayBlinkerSound(bool state)
    {
        _audioSource.PlayOneShot(
            state ? _blinkerOn : _blinkerOff, _blinkerVolumeMultiplier);
    }
}
