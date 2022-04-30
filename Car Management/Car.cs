using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Management
{
    internal class Car
    {
        private int year { get; set; }
        private String make { get; set; }
        private String model { get; set; }
        private String vin { get; set; }
        private String lp { get; set; }
        private int milage { get; set; }
        public Car(int year, String make, String model)
        { 
            this.year = year;
            this.make = make;
            this.model = model;
            vin = "";
            lp = "";
            milage = 0;
        }
        public Car GetCar()
        {
            return this;
        }
    }
}
