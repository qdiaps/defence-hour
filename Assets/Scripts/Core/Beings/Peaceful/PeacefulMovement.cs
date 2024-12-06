using UnityEngine;
using System.Collections;
using Config;
using DG.Tweening;

namespace Core.Beings.Peaceful
{
    public class PeacefulMovement : MonoBehaviour
    {
        private Tween _anim;
        private Vector3 _defaultScale;
        private Vector2 _direction;
        private Coroutine _activeCoroutine;
        private Rigidbody2D _rigidbody;
        private PeacefulConfigData _config;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _defaultScale = transform.localScale;
        }

        private void FixedUpdate() =>
            _rigidbody.linearVelocity = _direction;

        public void Construct(PeacefulConfigData config)
        {
            _config = config;
            StartMovement();
        }

        public void StartMovement()
        {
            StopAllMovement();
            _activeCoroutine = StartCoroutine(Move(_config.DownTime, 
                _config.Speed, _config.TimeMovement));
        }

        public void StopAllMovement()
        {
            if (_activeCoroutine != null)
            {
                StopCoroutine(_activeCoroutine);
                _activeCoroutine = null;
                StopMove();
            }
        }

        private IEnumerator Move(float downTime, float speed, float time)
        {
            while (true)
            {
                _anim = CreatePulseAnim(_config.PulseScale, _config.PulseDuration);
                _direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * speed;
                yield return new WaitForSeconds(time);
                StopMove();
                yield return new WaitForSeconds(downTime);
            }
        }

        private void StopMove()
        {
            StopAnim();
            _direction = Vector2.zero;
        }

        private Tween CreatePulseAnim(float pulseScale, float duration)
        {
            return transform.DOScale(_defaultScale * pulseScale, duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void StopAnim()
        {
            if (_anim != null && _anim.IsActive())
            {
                _anim.Kill();
                transform.localScale = _defaultScale;
            }
        }
    }
}
