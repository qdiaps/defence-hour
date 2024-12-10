using UnityEngine;
using Unity.Cinemachine;
using Config;
using Core.Beings.Peaceful;
using Core.Item;
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
        [SerializeField] private SwipeHandler _swipeHandler;
        [SerializeField] private StatsUpdater _statsUpdater;
        [SerializeField] private PauseHandler _pauseHandler;
        [SerializeField] private PeacefulSpawner _peacefulSpawner;
        [SerializeField] private PeacefulRemover _peacefulRemover;
        [SerializeField] private ButtonHandler _dashAttackHandler;
        [SerializeField] private ButtonHandler _tornadoAttackHandler;
        [SerializeField] private AttackCooldown _attackCooldownDash;
        [SerializeField] private AttackCooldown _attackCooldownTornado;
        [Header("Configs")]
        [SerializeField] private PeacefulSpawnerConfig _peacefulSpawnerConfig;
        [SerializeField] private PlayerConfig _playerConfig;

        private void Awake()
        {
            if (_playerPrefab == null || _playerSpawnPoint == null ||
                _joystickMovement == null || _tornadoAttackHandler == null ||
                _camera == null || _pauseHandler == null || _peacefulSpawner == null ||
                _peacefulSpawnerConfig == null || _dashAttackHandler == null ||
                _playerConfig == null || _peacefulRemover == null ||
                _attackCooldownDash == null || _attackCooldownTornado == null)
                Debug.LogError($"{name}: field(-s) is null!");

            var input = new InputHandler(_joystickMovement);
            var itemHelper = new ItemHelper();

            var player = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
            var playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.Construct(input, _pauseHandler, _playerConfig);
            player.GetComponent<PlayerStatsHandler>().Construct(_statsUpdater, _pauseHandler);
            player.GetComponent<PlayerAttack>().Construct(_dashAttackHandler, _tornadoAttackHandler, _playerConfig,
                _attackCooldownDash, _attackCooldownTornado);

            _camera.Follow = player.transform;

            _pauseHandler.AddComponent(playerMovement);

            _peacefulRemover.Construct(_peacefulSpawner);
            _peacefulSpawner.Construct(player.transform, _pauseHandler, _peacefulSpawnerConfig, _peacefulRemover);
        }
    }
}
