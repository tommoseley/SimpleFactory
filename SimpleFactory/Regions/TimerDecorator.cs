using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;

namespace SimpleFactory.Regions
{
    public class TimerDecorator : Region
    {
        private readonly Region region;
        private System.Timers.Timer Timer { get; set; }
        public TimerDecorator(Region region) : base(int X, int Y, ConsoleColor color) : this (X, Y, color)
        {
            this.region = region;
            this.Timer = new System.Timers.Timer(1000);
            this.Timer.Elapsed += OnTimerTick;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            Console.WriteLine("Timer ticked" + DateTime.Now.ToShortTimeString());

        }
        public override void UpdateText()
        {
            region.UpdateText();
        }
        public override void WriteText(string text)
        {
            region.WriteText(text);
        }
    }
}
