﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.Regions
{
    public abstract class Region
    {
        internal ConsoleState regionState { get; set; }
        public Region(int X, int Y, int width, int height, ConsoleColor color)
        {
            regionState = new ConsoleState(X, Y, width, height, color);              
        }
        public void UpdateRegionText()
        {
            ConsoleState state = SaveConsoleState();
            ClearRegion();
            regionState.SetState();
            UpdateText();
          //  RestoreConsoleState (state);
        }

//        public abstract void WriteText(string text); 
        public abstract void UpdateText();

        public void ClearRegion()
        {
            for (int i = regionState.Y; i < regionState.Y + regionState.Height; i++)
            {
                Console.SetCursorPosition(regionState.X, i);
                Console.Write(new string(' ', regionState.Width));
            }
        }

        internal struct ConsoleState
        {
            internal int X;

            internal int Y;
            public int Width { get; set; }
            public int Height { get; set; }

            internal ConsoleColor Color;
            internal void SetState()
            {
                Console.SetCursorPosition(X, Y);
                Console.ForegroundColor = Color;
            }
            public ConsoleState() : this(Console.CursorLeft, Console.CursorTop, Console.WindowWidth, Console.WindowHeight, Console.ForegroundColor)
            { }
            public ConsoleState(int X, int Y, int width, int height, ConsoleColor color)
            {
                this.X = X;
                this.Y = Y;
                this.Width = width;
                this.Height = height;   
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