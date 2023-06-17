using System;
using System.Collections;
using UnityEngine;

namespace Core.Animation
{
    public abstract class LinearAnimation<T>
    {
        protected static readonly WaitForSeconds _animationPause = new(0.016f);

        protected readonly Action<T> _applyStateAction;
        protected readonly Action _onAnimationCompleteAction;
        protected readonly T _start;
        protected readonly T _end;
        protected readonly float _speed;

        public LinearAnimation(T start, T end, float speed, Action<T> applyStateAction,
           Action onAnimationCompleteAction)
        {
            this._start = start;
            this._end = end;
            this._speed = speed;
            this._applyStateAction = applyStateAction;
            this._onAnimationCompleteAction = onAnimationCompleteAction;
        }

        public abstract IEnumerator GetAnimationCoroutine();
    }
}