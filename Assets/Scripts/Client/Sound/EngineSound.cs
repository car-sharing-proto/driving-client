using System;
using Core.Car;
using UnityEngine;

[Serializable]
public class EngineSound
{
    [SerializeField] private AnimationCurve _powerCurve;

    [SerializeField] private AudioSource _workingSource;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _engineWorkSound;
    [SerializeField] private AudioClip _engineStartSound;

    [SerializeField] private float _enginePitchMultiplier = 2.0f;
    [SerializeField] private float _engineVolumeMultiplier = 2.0f;

    private Engine _engine;

    public void Initialize(Engine engine)
    {
        _engine = engine;

        _workingSource.clip = _engineWorkSound;
        _workingSource.loop = true;
        _engine.Starter.OnChangeState += PlayStarterSound;

        _workingSource.Play();
    }

    public void Destroy()
    {
        _engine.Starter.OnChangeState -= PlayStarterSound;
    }

    public void Update()
    {
        var s = _engine.RPM / _engine.MaxRPM;

        _workingSource.pitch =
            _powerCurve.Evaluate(s) * _enginePitchMultiplier;
        _workingSource.volume = _engineVolumeMultiplier;
    }

    private void PlayStarterSound(EngineState state)
    {
        if (state == EngineState.STOPED)
        {
            return;
        }

        _audioSource.PlayOneShot(_engineStartSound, _engineVolumeMultiplier);
    }
}

