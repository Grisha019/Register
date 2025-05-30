using HeroAppNET.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroAppNET.Models.Inventory
{
    public class Inventory
    {
        public int Id { get; set; }
        private List<InventoryItem> _inventoryItems;

        public IEnumerable<InventoryItem> InventoryItems
        {
            get => _inventoryItems;
        }

        public Inventory()
        {
            _inventoryItems = new List<InventoryItem>();
        }

        public void AddItem(Item item)
        {
            InventoryItem? inventoryItem = _inventoryItems
                                .FirstOrDefault(x => x.Item.Id == item.Id);
            if (inventoryItem is null)
            {
                _inventoryItems.Add(new InventoryItem(item));
            }
            else
            {
                inventoryItem.Amount++;
            }
        }

    }
}
