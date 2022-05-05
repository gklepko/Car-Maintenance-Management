using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Car_Management
{
    public class Car
    {
        public Guid Key { get; set; } = Guid.NewGuid();
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string VIN { get; set; }
        public string LP { get; set; }
        public string Alias { get; set; }
        public string DisplayName => $"{Make} {Model} {Year} ({Alias})";

        private List<MaintenanceRecord> maintenanceRecords = new List<MaintenanceRecord>();

        public Car()
        {

        }
        public Car(string make, string model, int year, string alias)
        {
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.Alias = alias;
        }
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
