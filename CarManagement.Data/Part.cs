using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement.Data
{
    public class Part
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public Decimal PPU { get; set; } // price per unit
        public int Quantity { get; set; } = 1;
    }
}
