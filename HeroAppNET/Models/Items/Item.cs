using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroAppNET.Models.Items
{
    public abstract class Item
    {
        public int Id { get => _id; set => _id = value; }
        public string Title { get => _title; set => _title = value; }
        public string Description { get => _description; set => _description = value; }
        public string? Icon { get => _icon; set => _icon = value; }

        protected int _id;
        protected string _title;
        protected string _description;
        protected string _icon;

        public Item(int id, string title, string description)
        {
            _id = id;
            _title = title;
            _description = description;
        }

        public abstract void InvetoryUse();
    }
}
