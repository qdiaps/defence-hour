using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Config;
using Core.Services.PauseService;
using DG.Tweening;
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

        public void DeletePeaceful(GameObject peaceful)
        {
            if (_peacefuls.Contains(peaceful) == false)
                Debug.LogError($"{name}: try delete non created peaceful");
            peaceful.transform.DOScale(0, 1).SetEase(Ease.InBack)
                .OnComplete(() => 
            {
                _peacefuls.Remove(peaceful);
                Destroy(peaceful);
            });
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
                peaceful.GetComponentInChildren<PeacefulMovement>().Construct(_pause,
                    _config.Peacefuls[index]);
                peaceful.GetComponentInChildren<PeacefulHealth>().Construct(
                    _config.Peacefuls[index], this);
                _peacefuls.Add(peaceful);
            }
        }
    }
}
