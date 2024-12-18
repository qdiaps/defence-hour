namespace Core.Inventory
{
    public class InventorySlot
    {
        public readonly int ID;
        public readonly int Count;

        public InventorySlot(int id, int count)
        {
            ID = id;
            Count = count;
        }
    }
}
