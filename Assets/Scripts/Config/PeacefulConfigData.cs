using UnityEngine;
using System;

namespace Config
{
    [Serializable]
    public class PeacefulConfigData
    {
        public GameObject Prefab;
        public float SpawnChance;
        public float DownTime;
        [Header("Movement")]
        public float MinMovementRadius;
        public float MaxMovementRadius;
        public float TimeMovement;
        [Header("Run away")]
        public float TimeRunAway;
        public float RunAwayDistance;
    }
}
