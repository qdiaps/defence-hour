using UnityEngine;

namespace Core
{
    public interface IDamageable
    {
        public void OnDamage(GameObject source, float damage);
    }
}
