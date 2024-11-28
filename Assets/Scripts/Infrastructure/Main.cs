using UnityEngine;
using Core.Player;
using Core.Services.InputService;
using Core.UI.HUD.Movement;

namespace Infrastructure
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;

        private void Awake()
        {
            var fixedJoystick = FindFirstObjectByType<FixedJoystick>();

            var input = new InputHandler(fixedJoystick);

            Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity)
                .GetComponent<PlayerMovement>()
                .Construct(input);
        }
    }
}
