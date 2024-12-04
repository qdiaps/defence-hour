using UnityEngine;
using System.Collections;
using Core.Services.PauseService;
using Config;
using Extensions;

namespace Core.Beings.Peaceful
{
    public class NormalPeacefulMovement : MonoBehaviour
    {
        [SerializeField] private Transform _transform;

        private PauseHandler _pause;
        private Coroutine _coroutineMove = null;
        private Coroutine _coroutineSubMove = null;
        private Coroutine _coroutineRunAway = null;
        private Transform _escapeTarget; 

        protected PeacefulConfigData _config;

        public void Construct(PauseHandler pause, PeacefulConfigData config)
        {
            _pause = pause;
            _config = config;
            StartMovement();
        }

        public void StartMovement()
        {
            if (_coroutineMove == null)
                _coroutineMove = StartCoroutine(Move());
        }

        public void StopMovement()
        {
            if (_coroutineMove != null)
            {
                if (_coroutineSubMove != null)
                {
                    StopCoroutine(_coroutineSubMove);
                    _coroutineSubMove = null;
                }
                StopCoroutine(_coroutineMove);
                _coroutineMove = null;
            }
        }

        public void StartRunAway(Transform escapeTarget)
        {
            StopRunAway();
            StopMovement();
            _escapeTarget = escapeTarget;
            _coroutineRunAway = StartCoroutine(RunAway());
        }

        public void StopRunAway()
        {
            if (_coroutineRunAway != null)
            {
                StopCoroutine(_coroutineRunAway);
                _coroutineRunAway = null;
                _escapeTarget = null;
            }
            StartMovement();
        }

        private IEnumerator RunAway()
        {
            while (true)
            {
                yield return null;
                Vector2 directionFromEscape = (_transform.position - _escapeTarget.position)
                    .normalized;
                Vector2 targetPosition = (Vector2)_transform.position + directionFromEscape * 
                    _config.RunAwayDistance;
                yield return StartCoroutine(Move(targetPosition, _config.TimeRunAway));
            }
        }

        private IEnumerator Move()
        {
            while (true)
            {
                yield return new WaitForSeconds(_config.DownTime);
                Vector2 targetPosition = RandomUtility.GetRandomPositionInCirle(
                    _transform, _config.MinMovementRadius, _config.MaxMovementRadius);
                _coroutineSubMove = StartCoroutine(Move(targetPosition, _config.TimeMovement));
                yield return _coroutineSubMove;
                _coroutineSubMove = null;
            }
        }

        protected IEnumerator Move(Vector2 targetPosition, float time)
        {
            Vector2 startPosition = _transform.position;
            float elapsedTime = 0f;
            while (elapsedTime < time)
            {
                if (_pause.IsPause)
                {
                    yield return null;
                    continue;
                }
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / time;
                _transform.position = Vector2.Lerp(startPosition, targetPosition, progress);
                yield return null;
            }
            _transform.position = targetPosition;
        }
    }
}
