using System;
using System.Collections;
using UnityEngine;

namespace Core.Animation
{
    public class Vector3_LinearAnimation : LinearAnimation<Vector3>
    {
        private static readonly float eps = 0.0001f;

        public Vector3_LinearAnimation(Vector3 start, Vector3 end, float speed,
            Action<Vector3> applyStateAction, Action onAnimationCompleteAction)
            : base(start, end, speed, applyStateAction, onAnimationCompleteAction)
        {
        }

        public override IEnumerator GetAnimationCoroutine()
        {
            for (var t = .0f; t <= 1.0f + eps; t += _speed)
            {
                _applyStateAction?.Invoke(Vector3.Lerp(_start, _end, t));

                yield return _animationPause;
            }

            _onAnimationCompleteAction?.Invoke();
        }
    }
}