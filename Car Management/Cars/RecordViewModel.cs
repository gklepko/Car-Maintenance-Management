using CarManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Management.Cars
{
    public class RecordViewModel : BaseViewModel
    {
        public MaintenanceRecord Record { get; }
        public RecordViewModel(MaintenanceRecord record)
        {
            Record = record;
        }

    }
}
