using UnityEngine;
using Core.Player;
using Config;
using DG.Tweening;

namespace Core.Beings.Peaceful
{
    public class PeacefulHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private SpriteRenderer _sprite;

        private PeacefulRemover _remover;
        private PeacefulMovement _movement;
        private float _currentHealth;

        private PeacefulConfigData _config;

        private void Awake() =>
            _movement = GetComponent<PeacefulMovement>();

        public void Construct(PeacefulConfigData config, PeacefulRemover remover)
        {
            _config = config;
            _remover = remover;
            _currentHealth = _config.StartHealth;
        }

        public void OnDamage(GameObject source, float damage)
        {
            if (damage < 0)
                Debug.LogError($"{name}: damage value < 0");
            if (source.TryGetComponent<PlayerMovement>(out var player))
            {
                _currentHealth -= damage;
                if (_currentHealth <= 0)
                {
                    _movement.StopAllMovement();
                    Dead();
                    return;
                }
                _sprite.DOFade(0.5f, 0.2f).OnComplete(() => _sprite.DOFade(1f, 0.2f));
            }
        }

        public void Dead() =>
            _remover.RemoveWithAnim(gameObject);
    }
}
