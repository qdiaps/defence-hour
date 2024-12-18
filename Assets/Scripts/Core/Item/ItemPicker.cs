using UnityEngine;
using Core.Player;
using Core.Inventory;

namespace Core.Item
{
    public class ItemPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask _itemLayer;

        private PlayerStatsHandler _stats;
        private InventoryHandler _inventory;

        private void Awake() =>
            _stats = GetComponent<PlayerStatsHandler>();

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<ItemObject>(out var item))
            {
                if (_inventory.TryAddItem(out int remnant, item.ID, item.Count))
                {
                    if (remnant == 0)
                        Destroy(collider.gameObject);
                    else
                        item.Count = remnant;
                }
            }
        }

        public void Construct(InventoryHandler inventory) =>
            _inventory = inventory;
    }
}
