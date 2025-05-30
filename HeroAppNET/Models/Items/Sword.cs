using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeroAppNET.Models.Items
{
    public class Sword : Item
    {
        public Sword(int id, string title, string description)
            : base(id, title, description)
        {
        }

        public override void InvetoryUse()
        {
            MessageBox.Show($"Экпирировали {Title}");
        }
    }
}
