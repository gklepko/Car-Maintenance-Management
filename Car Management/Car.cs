using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Management
{
    internal class Car
    {
        public int Year { get; set; }
        public String Make { get; set; }
        public String Model { get; set; }
        public String VIN { get; set; }
        public String LP { get; set; }

        private List<MaintenanceRecord> maintenanceRecords = new List<MaintenanceRecord>();
        public MaintenanceRecord[] MaintenanceRecords 
        {
            get { return maintenanceRecords.ToArray(); } 
        }

        public void AddMaintenanceRecord(MaintenanceRecord record)
        { 
            maintenanceRecords.Add(record);
        }
    }
}
