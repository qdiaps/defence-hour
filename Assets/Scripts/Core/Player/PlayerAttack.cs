using UnityEngine;
using Core.Services.InputService;
using Core.UI.HUD.Movement;
using Config;
using System.Collections;
using DG.Tweening;

namespace Core.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private ButtonHandler _dashAttackHandler;
        private ButtonHandler _tornadoAttackHandler;
        private AttackCooldown _cooldownDashHandler;
        private AttackCooldown _cooldownTornadoHandler;
        private PlayerConfig _config;
        private bool _canDashAttack;
        private bool _canTornadoAttack;
        private bool _cooldownDash;
        private bool _cooldownTornado;

        private void OnDestroy()
        {
            _dashAttackHandler.OnClick -= OnDashAttack;
            _tornadoAttackHandler.OnClick -= OnTornadoAttack;
        }

        public void Construct(ButtonHandler dashAttackHandler, ButtonHandler tornadoAttackHandler,
            PlayerConfig config, AttackCooldown cooldownDash, AttackCooldown cooldownTornado)
        {
            _dashAttackHandler = dashAttackHandler;
            _dashAttackHandler.OnClick += OnDashAttack;
            _canDashAttack = true;
            _tornadoAttackHandler = tornadoAttackHandler;
            _tornadoAttackHandler.OnClick += OnTornadoAttack;
            _canTornadoAttack = true;
            _config = config;
            _cooldownDashHandler = cooldownDash;
            _cooldownTornadoHandler = cooldownTornado;
        }

        private void OnDashAttack()
        {
            if (_tornadoAttackHandler)
                StartCoroutine(DashAttack());
        }

        private IEnumerator DashAttack()
        {
            if (_canDashAttack == false)
                yield break;
            _canDashAttack = false;
            do
            {
                if (_cooldownDash)
                {
                    yield return null;
                    continue;
                }
                _cooldownDash = true;
                _cooldownDashHandler.StartAnim(_config.CooldownDash, () => _cooldownDash = false);
                Vector2 startPosition = transform.position;
                float angle = (transform.eulerAngles.z + 90f) * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
                RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, 
                    _config.DashDistance, _config.CollisionLayer);
                float targetDistance = hit.collider ? hit.distance : _config.DashDistance;
                Vector2 targetPosition = startPosition + direction * targetDistance;
                Sequence anim = DOTween.Sequence();
                anim.Append(transform.DOScaleY(_config.StretchFactor, _config.StretchDuration))
                    .Append(transform.DOScaleY(1f, _config.StretchDuration))
                    .Join(transform.DOLocalMove(targetPosition, 
                        targetDistance / _config.DashAttackSpeed))
                        .SetEase(Ease.OutQuad);
                yield return anim.WaitForCompletion();
                if (hit.collider != null && hit.collider.TryGetComponent<IDamageable>(out var damage))
                    damage.OnDamage(gameObject, _config.Damage);
                yield return null;
            } while (_dashAttackHandler.IsClick);
            _canDashAttack = true;
        }

        private void OnTornadoAttack()
        {
            if (_canDashAttack)
                StartCoroutine(TornadoAttack());
        }

        private IEnumerator TornadoAttack()
        {
            if (_canTornadoAttack == false)
                yield break;
            _canTornadoAttack = false;
            do 
            {
                if (_cooldownTornado)
                {
                    yield return null;
                    continue;
                }
                _cooldownTornado = true;
                _cooldownTornadoHandler.StartAnim(_config.CooldownTornado, () => _cooldownTornado = false);
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 
                    _config.TornadoAttackRadius, _config.CollisionLayer);
                foreach (var hit in hits)
                {
                    if (hit.TryGetComponent<IDamageable>(out var damage))
                        damage.OnDamage(gameObject, _config.Damage);
                }
                yield return transform
                    .DORotate(new Vector3(0, 0, _config.MaxSpinSpeed * _config.TornadoDuration), 
                        _config.TornadoDuration, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.OutQuad)
                    .WaitForCompletion();
                yield return null;
            } while (_tornadoAttackHandler.IsClick);
            _canTornadoAttack = true;
        }
    }
}
