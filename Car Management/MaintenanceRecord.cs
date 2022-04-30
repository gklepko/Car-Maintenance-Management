using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Management
{
    class MaintenanceRecord
    {
        public string Type { get; set; }
        public string Comments { get; set; }
        public Decimal Price { get; set; }
        public Part[] Parts { get; set; }
        public DateTime ServiceDate { get; set; } = DateTime.Now;
        public int MilageAtService { get; set; }
    }
}
