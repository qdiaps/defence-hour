using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName="PlayerConfig", menuName="Config/Player")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Movement")]
        public float MovementSpeed;
        public float PulseScale;
        public float PulseDuration;
        public float MinMagnitude;
        [Header("Attack")]
        public LayerMask CollisionLayer;
        public float Damage;
        [Header("Dash")]
        public float DashDistance;
        public float StretchFactor;
        public float StretchDuration;
        public float DashAttackSpeed;
        public float CooldownDash;
        [Header("Tornado")]
        public float TornadoDuration;
        public float MaxSpinSpeed;
        public float TornadoAttackRadius;
        public float CooldownTornado;
    }
}
