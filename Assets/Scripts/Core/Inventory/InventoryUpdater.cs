using UnityEngine;
using Core.Item;

namespace Core.Inventory
{
    public class InventoryUpdater : MonoBehaviour
    {
        [SerializeField] private InventorySlotUI[] _slots;

        private ItemHelper _helper;

        public void Construct(ItemHelper helper) =>
            _helper = helper;
        
        public void UpdateAllSlots(InventorySlot[] slotsData)
        {
            for (int i = 0; i < _slots.Length; i++)
                UpdateSlot(i, slotsData[i]);
        }

        public void UpdateSlot(int slotId, InventorySlot slotData)
        {
            InventorySlotUI slot = _slots[slotId];
            slot.Count.text = $"{slotData.Count}";
            if (slotData.ID == -1 && slot.Inv_Prefab != null)
            {
                Destroy(slot.Inv_Prefab);
                slot.Inv_Prefab = null;
            }
            else if (slotData.ID != -1 && slot.Inv_Prefab == null)
            {
                var item = _helper.GetItemFromID(slotData.ID);
                var prefab = item.Inv_Prefab;
                slot.Inv_Prefab = Instantiate(prefab, slot.Parent);
                if (item.IsStackable == false)
                    slot.Count.text = "";
            }
        }
    }
}
