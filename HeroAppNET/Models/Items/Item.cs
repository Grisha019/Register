using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroAppNET.Models.Items
{
    public abstract class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; } = string.Empty; // Fix: Assign default value to avoid null  

        protected Item(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
            Icon = string.Empty; // Fix: Initialize Icon with a default value  
        }

        public abstract void InvetoryUse();
    }
}
