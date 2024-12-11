using UnityEngine;
using Core.Player;
using Config;

namespace Core.Item
{
    public class ItemPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask _itemLayer;

        private PlayerStatsHandler _stats;

        private void Awake() =>
            _stats = GetComponent<PlayerStatsHandler>();

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<ItemObject>(out var item))
            {
                var config = item.Config;
                if (config.IsUsable && config.UsageEffectType == ItemConsumableType.Satiety)
                {
                    _stats.IncreaseSatiety(config.UsageEffectCount);
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
