using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.Regions
{
    public abstract class Region
    {
        public int  X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        internal ConsoleState regionState { get; set; }
        public Region(int X, int Y, ConsoleColor color)
        {
            regionState = new ConsoleState() { Color = color, X = X, Y = Y };              
        }
        public void UpdateRegionText()
        {
            ConsoleState state = SaveConsoleState();
            regionState.SetState();
            UpdateText();
            RestoreConsoleState (state);
        }

//        public abstract void WriteText(string text); 
        public abstract void UpdateText();

        internal struct ConsoleState
        {
            internal int X;
            internal int Y;
            internal ConsoleColor Color;
            internal void SetState()
            {
                Console.SetCursorPosition(X, Y);
                Console.ForegroundColor = Color;
            }
            public ConsoleState() : this(Console.CursorLeft, Console.CursorTop, Console.ForegroundColor)
            { }
            public ConsoleState(int X, int Y, ConsoleColor color)
            {
                this.X = X;
                this.Y = Y;
                this.Color = color;
            }

        }
  
        internal ConsoleState SaveConsoleState()
        {
            ConsoleState state = new ConsoleState();
            return state;
        }
        internal void RestoreConsoleState(ConsoleState state)
        {
            Console.SetCursorPosition(state.X, state.Y);
            Console.ForegroundColor = state.Color;
        }
    }
}