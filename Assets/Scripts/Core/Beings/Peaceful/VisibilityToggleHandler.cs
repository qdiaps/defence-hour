using UnityEngine;
using Core.Player;

namespace Core.Beings.Peaceful
{
    public class VisibilityToggleHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _body;
        [SerializeField] private PeacefulMovement _movement;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<PlayerMovement>(out var player))
            {
                _body.SetActive(true);
                _movement.StartMovement();
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.TryGetComponent<PlayerMovement>(out var player))
            {
                _movement.StopMovement();
                _body.SetActive(false);
            }
        }
    }
}
