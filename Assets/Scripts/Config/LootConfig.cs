using UnityEngine;
using System;

namespace Config
{
    [Serializable]
    public class LootConfig
    {
        public GameObject Prefab;
        [Range(0, 100)] public float DropChance;
        public int MinQuantity;
        public int MaxQuantity;
    }
}
