using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName="PeacefulSpawnerConfig", menuName="Config/PeacefulSpawner")]
    public class PeacefulSpawnerConfig : ScriptableObject
    {
        public float MinSpawnRadius;
        public float MaxSpawnRadius;
        public float MinTimeSpawn;
        public float MaxTimeSpawn;
        public int MaxActivePleaceful;
        public GameObject[] PeacefulPrefabs;

        [HideInInspector] public int PeacefulPrefabsCount;

        private void Awake() =>
            PeacefulPrefabsCount = PeacefulPrefabs.Length;
    }
}
