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
        [SerializeField] private FixedJoystick _joystickMovement;
        [SerializeField] private FixedJoystick _joystickRotation;

        private void Awake()
        {
            if (_playerPrefab == null || _playerSpawnPoint == null ||
                _joystickMovement == null || _joystickRotation == null)
                Debug.LogError($"{name}: field(-s) is null!");

            var input = new InputHandler(_joystickMovement, _joystickRotation);

            Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity)
                .GetComponent<PlayerMovement>()
                .Construct(input);
        }
    }
}
