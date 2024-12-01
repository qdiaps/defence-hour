using UnityEngine;
using Core.Services.PauseService;
using Core.Services.InputService;

namespace Core.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;
        private InputHandler _input;
        private PauseHandler _pause;
        private bool _canMove;

        private void Awake() =>
            _rigidbody = GetComponent<Rigidbody2D>();

        private void FixedUpdate()
        {
            if (_canMove == false)
                return;
            InputData input = _input.GetInput();
            _rigidbody.linearVelocity = input.Movement * _speed;
            _rigidbody.rotation = input.Rotation;
        }
        
        private void OnDestroy()
        {
            _pause.OnPause -= SetStop;
            _pause.OnPlay -= SetPlay;
        }

        public void Construct(InputHandler input, PauseHandler pause)
        {
            _input = input;
            _canMove = true;
            _pause = pause;
            _pause.OnPause += SetStop;
            _pause.OnPlay += SetPlay;
        }

        private void SetStop()
        {
            _canMove = false;
            _rigidbody.linearVelocity = Vector2.zero;
        }

        private void SetPlay() =>
            _canMove = true;
    }
}
