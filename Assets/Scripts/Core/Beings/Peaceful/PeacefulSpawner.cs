using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Config;
using Core.Services.PauseService;

namespace Core.Beings.Peaceful
{
    public class PeacefulSpawner : MonoBehaviour
    {
        private Transform _player;
        private PauseHandler _pause;
        private PeacefulSpawnerConfig _config;
        private List<GameObject> _pleacefuls = new List<GameObject>();

        public void Construct(Transform player, PauseHandler pause,
            PeacefulSpawnerConfig config)
        {
            _player = player;
            _pause = pause;
            _config = config;
            StartCoroutine(Generate());
        }

        private IEnumerator Generate()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_config.MinTimeSpawn, 
                    _config.MaxTimeSpawn));
                if (_pause.IsPause || _pleacefuls.Count >= _config.MaxActivePleaceful)
                    continue;
                Vector2 position = GetRandomPosition();
                GameObject prefab = _config.PeacefulPrefabs[Random.Range(0, 
                    _config.PeacefulPrefabsCount)];
                var pleaceful = Instantiate(prefab, position, Quaternion.identity);
                _pleacefuls.Add(pleaceful);
            }
        }

        private Vector2 GetRandomPosition()
        {
            float radius = Random.Range(_config.MinSpawnRadius, _config.MaxSpawnRadius);
            float angle = Random.Range(0, 360f);
            float x = _player.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = _player.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            return new Vector2(x, y);
        }
    }
}
