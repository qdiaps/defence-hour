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

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_input == null)
                return;
            _rigidbody.linearVelocity = _input.GetInput() * _speed;
        }

        public void Construct(InputHandler input)
        {
            _input = input;
        }
    }
}
