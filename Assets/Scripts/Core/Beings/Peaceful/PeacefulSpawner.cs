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
        private List<GameObject> _peacefuls = new List<GameObject>();
        private float[] _peacefulChances;

        public void Construct(Transform player, PauseHandler pause,
            PeacefulSpawnerConfig config)
        {
            _player = player;
            _pause = pause;
            _config = config;
            FillPeacefulChances();
            StartCoroutine(Generate());
        }

        private void FillPeacefulChances()
        {
            _peacefulChances = new float[_config.PeacefulPrefabsCount];
            for (int i = 0; i < _config.PeacefulPrefabsCount; i++)
                _peacefulChances[i] = _config.Peacefuls[i].SpawnChance;
        }

        private IEnumerator Generate()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_config.MinTimeSpawn, 
                    _config.MaxTimeSpawn));
                if (_pause.IsPause || _peacefuls.Count >= _config.MaxActivePleaceful)
                    continue;
                Vector2 position = RandomUtility.GetRandomPositionInCirle(
                    _player, _config.MinSpawnRadius, _config.MaxSpawnRadius);
                int index = RandomUtility.GetRandomIndexFromListChances(_peacefulChances);
                if (index == -1)
                {
                    Debug.LogError($"{name}: RandomUtility.GetRandomIndexFromListChances returned -1");
                    continue;
                }
                GameObject prefab = _config.Peacefuls[index].Prefab;
                var peaceful = Instantiate(prefab, position, Quaternion.identity);
                peaceful.GetComponentInChildren<NormalPeacefulMovement>().Construct(_pause,
                    _config.Peacefuls[index]);
                _peacefuls.Add(peaceful);
            }
        }
    }
}
