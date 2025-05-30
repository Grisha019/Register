using HeroAppNET.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroAppNET.Models.Inventory
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public Item Item { get => _item; }
        public int Amount { get => _amount; set => _amount = value; }

        private Item _item;
        private int _amount;

        public InventoryItem(Item item, int amount = 1)
        {
            if (amount < 1)
                throw new ArgumentException("Amount не может быть меньше 0",
                    nameof(amount));

            _item = item;
            _amount = amount;
        }

        public InventoryItem()
        {

        }


    }
}
