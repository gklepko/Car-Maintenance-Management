using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarManagement.Data
{
    public class MaintenanceRecord
    {
        [JsonIgnore]
        public Car Car { get; set; }
        
        public Guid Key { get; set; } = Guid.NewGuid();
        public string Type { get; set; }
        public string Comments { get; set; }
        public Decimal Price { get; set; }
        public Part[] Parts { get; set; }
        public DateTime ServiceDate { get; set; } = DateTime.Today;
        public int MilageAtService { get; set; }
        public DateTime InspectionDate { get; set; } = DateTime.Now;
        public DateTime ServiceReminder { get; set; } = DateTime.Now.AddMonths(3);  // user to notify user of upcoming service

        public MaintenanceRecord() { }

        public MaintenanceRecord(Car car)
        {
            Car = car;
        }
    }
}
