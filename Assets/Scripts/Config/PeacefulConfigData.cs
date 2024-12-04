using UnityEngine;
using System;

namespace Config
{
    [Serializable]
    public class PeacefulConfigData
    {
        public GameObject Prefab;
        public float MinMovementRadius;
        public float MaxMovementRadius;
        public float MovementSpeed;
        public float DownTime;
        public float SpawnChance;
    }
}
