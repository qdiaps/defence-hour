using UnityEngine;
using System.Collections;
using Core.Services.PauseService;
using Config;
using Extensions;

namespace Core.Beings.Peaceful
{
    public class NormalPeacefulMovement : MonoBehaviour
    {
        private PauseHandler _pause;
        private PeacefulConfigData _config;
        private Coroutine _coroutine = null;

        public void Construct(PauseHandler pause, PeacefulConfigData config)
        {
            _pause = pause;
            _config = config;
            StartMovement();
        }

        public void StartMovement()
        {
            if (_coroutine == null)
                _coroutine = StartCoroutine(Move());
        }

        public void StopMovement()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator Move()
        {
            while (true)
            {
                yield return new WaitForSeconds(3f);
                Vector2 targetPosition = RandomUtility.GetRandomPositionInCirle(
                    transform, 3, 5);
                yield return StartCoroutine(Move(targetPosition, 4));
            }
        }

        private IEnumerator Move(Vector2 targetPosition, float time)
        {
            Vector2 startPosition = transform.position;
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
                transform.position = Vector2.Lerp(startPosition, targetPosition, progress);
                yield return null;
            }
            transform.position = targetPosition;
        }
    }
}
