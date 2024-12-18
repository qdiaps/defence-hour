using Config;
using Core.Item;

namespace Core.Inventory
{
    public class InventoryHandler
    {
        private readonly InventorySlot[] _slots;
        private readonly InventoryConfig _config;
        private readonly InventoryUpdater _updater;
        private readonly ItemHelper _helper;

        public InventoryHandler(InventoryConfig config, InventoryUpdater updater,
            ItemHelper helper)
        {
            _config = config;
            _updater = updater;
            _helper = helper;
            _slots = new InventorySlot[_config.SlotCount];
            for (int i = 0; i < _slots.Length; i++)
                _slots[i] = new InventorySlot(-1, 0);
            _updater.UpdateAllSlots(_slots);
        }

        public bool TryAddItem(out int remnant, int id, int count)
        {
            var item = _helper.GetItemFromID(id);
            for (int i = 0; i < _slots.Length; i++)
            {
                if (TryAddItem(out remnant, id, count, i))
                {
                    if (remnant != 0 && i != _slots.Length - 1)
                    {
                        for (int j = i + 1; j < _slots.Length; j++)
                        {
                            if (TryAddItem(out remnant, id, remnant, j))
                                return true;
                        }
                    }
                    return true;
                }
            }
            remnant = count;
            return false;
        }

        public bool TryAddItem(out int remnant, int id, int count, int slotId)
        {
            var slot = _slots[slotId];
            var item = _helper.GetItemFromID(id);
            remnant = count;
            if (slot.ID == -1)
            {
                UpdateSlotData(id, count, slotId);
                remnant = 0;
                return true;
            }
            else if (slot.ID == id && item.IsStackable && slot.Count < item.StackLimit)
            {
                remnant = 0;
                var newCount = slot.Count + count;
                if (newCount > item.StackLimit)
                {
                    remnant = newCount % item.StackLimit;
                    newCount -= remnant;
                }
                UpdateSlotData(id, newCount, slotId);
                return true;
            }
            return false;
        }

        public bool TryDecreaseItem(int id, int count, int slotId)
        {
            var item = _helper.GetItemFromID(id);
            var slot = _slots[slotId];
            if (slot.ID != id && slot.Count < count)
                return false;
            UpdateSlotData(id, slot.Count - count, slotId);
            return true;
        }

        public InventorySlot GetItem(int slotId) =>
            _slots[slotId];

        public void RemoveItem(int slotId)
        {
            _slots[slotId] = new InventorySlot(-1, 0);
            _updater.UpdateSlot(slotId, _slots[slotId]);
        }

        private void UpdateSlotData(int id, int count, int slotId)
        {
            _slots[slotId] = new InventorySlot(id, count);
            _updater.UpdateSlot(slotId, _slots[slotId]);
        }
    }
}
