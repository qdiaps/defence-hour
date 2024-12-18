using System;
using UnityEngine;
using TMPro;

namespace Core.Inventory
{
    [Serializable]
    public class InventorySlotUI
    {
        public RectTransform Parent;
        public GameObject Inv_Prefab;
        public TMP_Text Count;
    }
}
