using UnityEngine;
using System;

namespace Config
{
    [Serializable]
    public class PeacefulConfigData
    {
        public GameObject Prefab;
        [Range(0, 100)] public float SpawnChance;
        public float DownTime;
        public float StartHealth;
        [Header("Movement")]
        public float TimeMovement;
        public float Speed;
        public float PulseScale;
        public float PulseDuration;
        [Header("Loot")]
        public LootConfig[] Loot;
    }
}
