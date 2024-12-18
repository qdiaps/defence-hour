using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName="InventoryConfig", menuName="Config/Inventory")]
    public class InventoryConfig : ScriptableObject
    {
        public int SlotCount;
    }
}
