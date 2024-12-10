using UnityEngine;
using Config;

namespace Core.Item
{
    public class ItemObject : MonoBehaviour
    {
        public int ID { get; private set; }

        [SerializeField] private ItemConfig _config;

        private void Awake() =>
            ID = _config.Id;
    }
}
