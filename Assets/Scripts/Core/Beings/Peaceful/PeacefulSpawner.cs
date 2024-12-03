using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Config;
using Core.Services.PauseService;
using Extensions;

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
                Vector2 position = RandomUtility.GetRandomPositionInCirle(
                    _player, _config.MinSpawnRadius, _config.MaxSpawnRadius);
                int index = Random.Range(0, _config.PeacefulPrefabsCount);
                GameObject prefab = _config.Peacefuls[index].Prefab;
                var peaceful = Instantiate(prefab, position, Quaternion.identity);
                peaceful.GetComponentInChildren<NormalPeacefulMovement>().Construct(_pause,
                    _config.Peacefuls[index]);
                _pleacefuls.Add(peaceful);
            }
        }
    }
}
