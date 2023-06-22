using UnityEngine;

public class SmoothPressing
{
    private readonly float _speed = 0.1f;

    public float Value { get; private set; }

    public SmoothPressing(float speed)
    {
        this._speed = speed;
    }

    public void Press()
    {
        if (Value < 1)
        {
            Value += _speed * Time.deltaTime;
        }
        else
        {
            Value = 1;
        }
    }

    public void Release()
    {
        if (Value > 0)
        {
            Value -= _speed * Time.deltaTime;
        }
        else
        {
            Value = 0;
        }
    }
}
