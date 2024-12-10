using UnityEngine;
using Config;
using System.Linq;

namespace Core.Item
{
    public class ItemHelper
    {
        private ItemConfig[] _items;

        public ItemHelper() =>
            _items = Resources.LoadAll<ItemConfig>("ItemConfigs");

        public ItemConfig GetItemFromID(int id) =>
            _items.FirstOrDefault(item => item.Id == id);
    }
}
