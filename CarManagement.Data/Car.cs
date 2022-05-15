using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarManagement.Data
{
    public class Car
    {
        public Guid Key { get; set; } = Guid.NewGuid();
        public int Year { get; set; } = DateTime.Now.Year;
        public string Make { get; set; }
        public string Model { get; set; }
        public string VIN { get; set; }
        public string LP { get; set; }
        public string Alias { get; set; }
        public string DisplayName => $"{Make} {Model} {Year} ({Alias})";

        [JsonIgnore]
        private List<MaintenanceRecord> maintenanceRecords = new List<MaintenanceRecord>();

        [JsonIgnore]
        public MaintenanceRecord[] MaintenanceRecords { get { return maintenanceRecords.ToArray(); } }

        public Car() { }
        public Car(string make, string model, int year, string alias)
        {
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.Alias = alias;
        }
        public void AddMaintenanceRecord(MaintenanceRecord record)
        { 
            maintenanceRecords.Add(record);
        }

        public void RemoveMaintenanceRecord(MaintenanceRecord record)
        {
            maintenanceRecords.Remove(record);
        }
    }
}
