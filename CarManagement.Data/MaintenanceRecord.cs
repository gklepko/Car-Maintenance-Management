using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Management
{
    public class MaintenanceRecord
    {
        public string Type { get; set; }
        public string Comments { get; set; }
        public Decimal Price { get; set; }
        public Part[] Parts { get; set; }
        public DateTime ServiceDate { get; set; } = DateTime.Today;
        public int MilageAtService { get; set; }
        public DateTime InspectionDate { get; set; } = DateTime.Now;
        public DateTime ServiceReminder { get; set; } = DateTime.Now;  // user to notify user of upcoming service
    }
}
