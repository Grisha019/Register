using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HeroAppNET.Models.Utilty
{
    public abstract class GameObject : ObservableObject
    {
        private int _x;
        private int _y;

        private int _width;
        private int _height;
        private Brush _color = Brushes.Transparent; // Initialize _color to a default value  

        private int _dx;
        private int _dy;

        public int X
        {
            get => _x;
            set => Set(ref _x, value);
        }
        public int Y
        {
            get => _y;
            set => Set(ref _y, value);
        }

        public int Width
        {
            get => _width;
            set => Set(ref _width, value);
        }
        public int Height
        {
            get => _height;
            set => Set(ref _height, value);
        }
        [NotMapped]
        public Brush Color
        {
            get => _color;
            set => Set(ref _color, value);
        }

        public int Dx
        {
            get => _dx;
            set => Set(ref _dx, value);
        }
        public int Dy
        {
            get => _dy;
            set => Set(ref _dy, value);
        }

        public abstract void Update();
    }
}
