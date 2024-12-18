using UnityEngine;
using Unity.Cinemachine;
using Core.Player;
using Core.Services.InputService;
using Core.Services.PauseService;
using Core.UI.HUD.Movement;
using Core.UI.HUD.Stats;

namespace Infrastructure
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private CinemachineCamera _camera;
        [Header("Scripts")]
        [SerializeField] private FixedJoystick _joystickMovement;
        [SerializeField] private FixedJoystick _joystickRotation;
        [SerializeField] private StatsUpdater _statsUpdater;
        [SerializeField] private PauseHandler _pauseHandler;

        private void Awake()
        {
            if (_playerPrefab == null || _playerSpawnPoint == null ||
                _joystickMovement == null || _joystickRotation == null ||
                _camera == null || _pauseHandler == null)
                Debug.LogError($"{name}: field(-s) is null!");

            var input = new InputHandler(_joystickMovement, _joystickRotation);

            var player = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
            player.GetComponent<PlayerMovement>().Construct(input);
            player.GetComponent<PlayerStatsHandler>().Construct(_statsUpdater, _pauseHandler);

            _camera.Follow = player.transform;
        }
    }
}
