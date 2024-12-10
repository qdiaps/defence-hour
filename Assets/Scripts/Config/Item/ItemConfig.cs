using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Config
{
    [CreateAssetMenu(fileName="ItemConfig", menuName="Config/Item")]
    public class ItemConfig : ScriptableObject
    {
        [Header("Info")]
        public int Id;
        public string Name;
        public string Desctiption;
        public ItemRarity Rarity;
        
        [Header("Visual")]
        public GameObject Prefab;
        public Image Icon;
        
        [Header("Stack")]
        [HideInInspector] public bool IsStackable;
        [HideInInspector] public int StackLimit;

        [Header("Usage")]
        [HideInInspector] public bool IsUsable;
        [HideInInspector] public ItemUsageType UsageType;
        [HideInInspector] public ItemConsumableType UsageEffectType;
        [HideInInspector] public int UsageEffectCount;

        [Header("Craft")]
        [HideInInspector] public bool IsCraftable;
        [HideInInspector] public ItemConfig[] Ingredients;
        [HideInInspector] public int CraftedItemCount;

        private void OnValidate()
        {
            Id = Id < 0 ? 0 : Id;
            StackLimit = StackLimit < 1 ? 1 : StackLimit;
            CraftedItemCount = CraftedItemCount < 1 ? 1 : CraftedItemCount;
        }
    }
}
