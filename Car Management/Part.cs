using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Management
{
    internal class Part
    {
        private string manufacturer { get; set; }
        private string model { get; set; }
        private double ppu { get; set; } // price per unit
        private int quanity { get; set; }

        public Part(string manufacturer, string model, double ppu, int quantity)
        {
            this.manufacturer = manufacturer;
            this.model = model;
            this.ppu = ppu;
            this.quanity = quantity;
        }
    }
}
