using UnityEngine;

public class SmoothPressing
{
    private readonly float _pressSpeed = 0.1f;
    private readonly float _releaseSpeed = 0.1f;
    private readonly float _middleValue = 0.5f;

    public bool FullPush { get; set; } = false;
    public float Value { get; private set; }

    public SmoothPressing(float pressSpeed,
        float releaseSpeed, float middleValue = 0.5f)
    {
        this._pressSpeed = pressSpeed;
        this._releaseSpeed = releaseSpeed;
        this._middleValue = middleValue;
    }

    public void Press()
    {
        var press = FullPush ? 1 : _middleValue;

        if (Value < press)
        {
            Value += _pressSpeed * Time.deltaTime;
        }
        else if (Value > press + _releaseSpeed * Time.deltaTime * 2.0f)
        {
            Value -= _releaseSpeed * Time.deltaTime;
        }
    }

    public void Release()
    {
        if (Value > 0)
        {
            Value -= _releaseSpeed * Time.deltaTime;
        }
        else
        {
            Value = 0;
        }
    }
}
