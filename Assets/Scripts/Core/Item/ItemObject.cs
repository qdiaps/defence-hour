using UnityEngine;
using Config;

namespace Core.Item
{
    public class ItemObject : MonoBehaviour
    {
        public int Count = 1;

        public int ID { get; private set; }

        [field: SerializeField] public ItemConfig Config { get; private set; }

        private void Awake() =>
            ID = Config.Id;
    }
}
