using System.Collections.Generic;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Inventory
{
    public class Bag : IBag
    {
        public int Capacity { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public int Load => throw new System.NotImplementedException();

        public IReadOnlyCollection<Item> Items => throw new System.NotImplementedException();

        public void AddItem(Item item)
        {
            throw new System.NotImplementedException();
        }

        public Item GetItem(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
