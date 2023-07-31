﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.Regions
{
    public class InventoryRegion : Region
    {
        Inventory inventory;
        public InventoryRegion(int X, int Y, int width, int height, ConsoleColor color, Inventory inventory) : base(X, Y, width, height, color)
        {
            this.inventory = inventory;
        }
        public override void UpdateText()
        {
            ClearRegion();
            int LineNumber = regionState.Y;
            Console.SetCursorPosition(regionState.X, LineNumber++);
            Console.Write("Inventory:");
            Console.SetCursorPosition(regionState.X, LineNumber++);
            foreach (string key in inventory.Items.Keys)
            {
                Console.WriteLine($"{key} - {inventory.Items[key].Quantity}");
                Console.SetCursorPosition(regionState.X, LineNumber++);
            }
        }
        //private void ClearRegion()
        //{
        //    for (int i = regionState.Y; i < regionState.Y + regionState.Height; i++)
        //    {
        //        Console.SetCursorPosition(regionState.X, i);
        //        Console.Write(new string(' ', regionState.Width));
        //    }
        //}
    }
}
