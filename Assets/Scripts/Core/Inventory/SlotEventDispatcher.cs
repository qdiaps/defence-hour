using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using Core.Services.InputService;
using Core.Item;
using System.Linq;
using System.Collections.Generic;

namespace Core.Inventory
{
    public class SlotEventDispatcher : MonoBehaviour
    {
        private SlotEventHandler _activeHandler;
        private GameObject _itemPrefab;
        private InventoryHandler _inventory;
        private ItemHelper _helper;
        private int _defaultSortingLayer;

        private void Update()
        {
            if (_activeHandler == null || _activeHandler.DragEventData == null)
                return;

            var position = Camera.main.ScreenToWorldPoint(_activeHandler.DragEventData.position);
            position.z = 0;
            _itemPrefab.transform.position = position;
        }

        public void Construct(InventoryHandler inventory, ItemHelper helper)
        {
            _inventory = inventory;
            _helper = helper;
        }

        public void StartDrag(SlotEventHandler handler)
        {
            if (_activeHandler != null)
                return;

            var item = _inventory.GetItem(handler.ID);
            if (item.ID != -1)
            {
                _activeHandler = handler;
                var position = Camera.main.ScreenToWorldPoint(handler.DragEventData.position);
                position.z = 0;
                var itemConfig = _helper.GetItemFromID(item.ID);
                _itemPrefab = Instantiate(itemConfig.Prefab, position, Quaternion.identity);
                _itemPrefab.GetComponents<Collider2D>().FirstOrDefault(col => col.enabled == true)
                   .enabled = false;
                var sortingGroup = _itemPrefab.GetComponent<SortingGroup>();
                _defaultSortingLayer = sortingGroup.sortingOrder;
                sortingGroup.sortingOrder = 1000;
            }
        }

        public void StopDrag(SlotEventHandler handler)
        {
            if (_activeHandler != null && _activeHandler == handler)
            {
                var item = _inventory.GetItem(handler.ID);
                var from = handler.ID;
                List<RaycastResult> result = new List<RaycastResult>();
                EventSystem.current.RaycastAll(handler.DragEventData, result);
                foreach (var raycast in result)
                {
                    if (raycast.gameObject.TryGetComponent<SlotEventHandler>(out var slot))
                    {
                        Destroy(_itemPrefab);
                        if (slot != handler)
                        {
                            var to = slot.ID;
                            if (_inventory.TryAddItem(out int remnant, item.ID, item.Count, to))
                            {
                                if (remnant == 0)
                                    _inventory.RemoveItem(from);
                                else
                                    _inventory.TryDecreaseItem(item.ID, item.Count - remnant, from);
                            }
                        }
                        Reset();
                        return;
                    }
                }
                _itemPrefab.GetComponent<SortingGroup>().sortingOrder = _defaultSortingLayer;
                _itemPrefab.GetComponent<ItemObject>().Count = item.Count;
                _inventory.RemoveItem(from);
                var colliders = _itemPrefab.GetComponents<Collider2D>();
                foreach (var collider in colliders)
                    collider.enabled = true;
                Reset();
            }
        }

        private void Reset()
        {
            _activeHandler = null;
            _itemPrefab = null;
        }
    }
}
