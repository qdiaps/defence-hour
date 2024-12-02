using UnityEngine;
using Core.Services.InputService;
using Core;
using DG.Tweening;

namespace Core.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _body1;
        [SerializeField] private GameObject _body2;
        [SerializeField] private GameObject _body3;
        [SerializeField] private float _attackRange;
        [SerializeField] private LayerMask _targetLayer;

        private AttackHandler _attackHandler;
        private bool _canAttack;

        private void OnDestroy() =>
            _attackHandler.OnAttack -= OnAttack;

        private void OnDrawGizmos()
        {
            Vector2 direction = transform.TransformDirection(Vector2.up);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction * _attackRange);
        }

        public void Construct(AttackHandler attackHandler)
        {
            _attackHandler = attackHandler;
            _attackHandler.OnAttack += OnAttack;
            _canAttack = true;
        }

        private void OnAttack()
        {
            if (_canAttack == false)
                return;
            Attack();
        }

        private void Attack()
        {
            _canAttack = false;

            Sequence anim = DOTween.Sequence();
            _body1.SetActive(true);
            anim
                .Append(_body1.transform.DOLocalMoveY(0.35f, 0.15f).From(0f).SetEase(Ease.Linear))
                .Join(_body1.transform.DOScale(0.8f, 0.15f).From(1f).SetEase(Ease.Linear))
                .OnComplete(() =>
            {
                Sequence anim = DOTween.Sequence();
                _body2.SetActive(true);
                anim
                    .Append(_body2.transform.DOLocalMoveY(0.7f, 0.15f).From(0.35f).SetEase(Ease.Linear))
                    .Join(_body2.transform.DOScale(0.6f, 0.15f).From(0.8f).SetEase(Ease.Linear))
                    .OnComplete(() =>
                {
                    Sequence anim = DOTween.Sequence();
                    _body3.SetActive(true);
                    anim
                        .Append(_body3.transform.DOLocalMoveY(1.1f, 0.15f).From(0.7f).SetEase(Ease.Linear))
                        .Join(_body3.transform.DOScale(0.4f, 0.15f).From(0.6f).SetEase(Ease.Linear))
                        .OnComplete(() =>
                    {
                        _body1.SetActive(false);
                        _body2.SetActive(false);
                        _body3.SetActive(false);
                        CheckHitArea();
                        if (_attackHandler.IsAttack)
                            Attack();
                        else
                            _canAttack = true;
                    });
                });
            });
        }

        private void CheckHitArea()
        {
            Vector2 direction = transform.TransformDirection(Vector2.up);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 
                _attackRange, _targetLayer);
            if (hit.collider == null)
                return;
            //hit.collider.GetComponent<IDamageable>().OnDamage();
            Debug.Log($"{hit.collider.name}.OnDamage");
        }
    }
}
