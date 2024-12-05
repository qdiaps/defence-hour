using UnityEngine;
using Core.Services.PauseService;
using Core.Services.InputService;
using Config;
using DG.Tweening;

namespace Core.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private InputHandler _input;
        private PauseHandler _pause;
        private PlayerConfig _config;
        private bool _canMove;
        private float _angle;
        private Vector3 _defaultScale;
        private Tween _pulseAnim;

        private void Awake() =>
            _rigidbody = GetComponent<Rigidbody2D>();

        private void FixedUpdate()
        {
            if (_canMove == false)
                return;
            InputData input = _input.GetInput();
            if (input.Movement.magnitude < _config.MinMagnitude)
            {
                _rigidbody.linearVelocity = Vector2.zero;
                return;
            }
            _rigidbody.linearVelocity = input.Movement * _config.MovementSpeed;
            _rigidbody.rotation = input.Rotation - 90f;
        }

        private void Update()
        {
            if (_canMove == false)
                return;
            if (_rigidbody.linearVelocity.magnitude > _config.MinMagnitude)
                StartPulsing();
            else
                StopPulsing();
        }
        
        private void OnDestroy()
        {
            _pause.OnPause -= SetStop;
            _pause.OnPlay -= SetPlay;
        }

        public void Construct(InputHandler input, PauseHandler pause, PlayerConfig config)
        {
            _defaultScale = transform.localScale;
            _input = input;
            _canMove = true;
            _pause = pause;
            _pause.OnPause += SetStop;
            _pause.OnPlay += SetPlay;
            _config = config;
        }

        private void SetStop()
        {
            _canMove = false;
            _rigidbody.linearVelocity = Vector2.zero;
        }

        private void SetPlay() =>
            _canMove = true;

        private void StartPulsing()
        {
            if (_pulseAnim == null || _pulseAnim.IsActive() == false ||
                _pulseAnim.IsPlaying() == false)
            {
                _pulseAnim = transform.DOScale(_defaultScale * _config.PulseScale, 
                    _config.PulseDuration)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
            }
        }

        private void StopPulsing()
        {
            if (_pulseAnim != null && _pulseAnim.IsActive())
            {
                _pulseAnim.Kill();
                transform.localScale = _defaultScale;
            }
        }
    }
}
