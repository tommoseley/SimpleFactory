using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.Regions
{
    public abstract class Region
    {
        public bool IsVisible { get; set; }
        public int X;

        public int Y;
        public int Width { get; set; }
        public int Height { get; set; }

        internal ConsoleColor Color;
        public Region()
        {
            contents = new List<string>();
        }
        public Region(int X, int Y, int width, int height, bool isVisible, ConsoleColor color) : base ()
        {
            IsVisible = isVisible;
            this.X = X;
            this.Y = Y;
            this.Width = width;
            this.Height = height;
            this.Color = color;
        }
        public List<string> contents;        
        public void UpdateRegion()
        {

            Console.ForegroundColor = Color;
            UpdateText();
        }

        public abstract void UpdateText();

        public void ClearRegion()
        {
            for (int i = Y; i < Y + Height; i++)
            {
                Console.SetCursorPosition(X, i);
                Console.Write(new string(' ', Width));
            }
        }
    }
}