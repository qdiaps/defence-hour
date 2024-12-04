using UnityEngine;
using Core.Player;

namespace Core.Beings.Peaceful
{
    public class LightPeacefulMovement : NormalPeacefulMovement
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<PlayerMovement>(out var player))
                StartRunAway(collider.gameObject.transform);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.TryGetComponent<PlayerMovement>(out var player))
                StopRunAway();
        }
    }
}
