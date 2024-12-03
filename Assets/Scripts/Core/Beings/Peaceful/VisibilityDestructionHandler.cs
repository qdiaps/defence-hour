using UnityEngine;
using Core.Player;

namespace Core.Beings.Peaceful
{
    public class VisibilityDestructionHandler : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.TryGetComponent<PlayerMovement>(out var player))
                Debug.Log("Destruction");
        }
    }
}
