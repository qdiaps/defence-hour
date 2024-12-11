using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Config;
using Core.Services.PauseService;
using Core.Loot;
using Extensions;

namespace Core.Beings.Peaceful
{
    public class PeacefulSpawner : MonoBehaviour
    {
        private Transform _point;
        private PauseHandler _pause;
        private PeacefulSpawnerConfig _config;
        private PeacefulRemover _remover;
        private LootSpawner _loot;
        private List<GameObject> _peacefuls = new List<GameObject>();
        private float[] _peacefulChances;

        public void Construct(Transform point, PauseHandler pause,
            PeacefulSpawnerConfig config, PeacefulRemover remover, LootSpawner loot)
        {
            _point = point;
            _pause = pause;
            _config = config;
            _remover = remover;
            _loot = loot;
            FillPeacefulChances();
            StartCoroutine(Generate());
        }

        public void DeletePeaceful(GameObject peaceful)
        {
            if (_peacefuls.Contains(peaceful) == false)
                Debug.LogError($"{name}: try delete non created peaceful");
            _peacefuls.Remove(peaceful);
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
                    _point, _config.MinSpawnRadius, _config.MaxSpawnRadius);
                int index = RandomUtility.GetRandomIndexFromListChances(_peacefulChances);
                if (index == -1)
                {
                    Debug.LogError($"{name}: RandomUtility.GetRandomIndexFromListChances returned -1");
                    continue;
                }
                GameObject prefab = _config.Peacefuls[index].Prefab;
                var peaceful = Instantiate(prefab, position, Quaternion.identity);
                _peacefuls.Add(peaceful);
                Resolve(peaceful, _config.Peacefuls[index]);
            }
        }

        private void Resolve(GameObject prefab, PeacefulConfigData config)
        {
            prefab.GetComponentInChildren<PeacefulMovement>().Construct(config);
            prefab.GetComponentInChildren<PeacefulHealth>().Construct(config, _remover, _loot);
        }
    }
}
