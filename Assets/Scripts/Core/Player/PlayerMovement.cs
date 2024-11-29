using UnityEngine;
using Core.Services.InputService;

namespace Core.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;
        private InputHandler _input;

        private void Awake() =>
            _rigidbody = GetComponent<Rigidbody2D>();

        private void FixedUpdate()
        {
            if (_input == null)
                return;
            InputData input = _input.GetInput();
            _rigidbody.linearVelocity = input.Movement * _speed;
            _rigidbody.rotation = input.Rotation;
        }

        public void Construct(InputHandler input) =>
            _input = input;
    }
}
