using UnityEngine;
using Core.Player;
using Config;

namespace Core.Beings.Peaceful
{
    public class PeacefulHealth : MonoBehaviour, IDamageable
    {
        public bool IsDead { get; private set; } = false;

        [SerializeField] private GameObject _peaceful;

        private PeacefulMovement _movement;
        private PeacefulSpawner _spawner;
        private float _currentHealth;

        protected PeacefulConfigData _config;

        private void Awake() =>
            _movement = GetComponent<PeacefulMovement>();

        public void Construct(PeacefulConfigData config, PeacefulSpawner spawner)
        {
            _config = config;
            _currentHealth = _config.StartHealth;
            _spawner = spawner;
        }

        public virtual void OnDamage(GameObject source, float damage)
        {
            if (damage < 0)
                Debug.LogError($"{name}: damage value < 0");
            if (source.TryGetComponent<PlayerMovement>(out var player))
            {
                _currentHealth -= damage;
                if (_currentHealth <= 0)
                {
                    Dead();
                    return;
                }
                _movement.StartRunAway(source.transform, true);
            }
        }

        public void Dead()
        {
            IsDead = true;
            _spawner.DeletePeaceful(_peaceful);
        }
    }
}
