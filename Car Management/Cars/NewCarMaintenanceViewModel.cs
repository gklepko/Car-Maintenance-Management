using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Management.Cars
{
    public class NewCarMaintenanceViewModel : BaseViewModel
    {
        private readonly Action<MaintenanceRecord> record;
        private readonly Action cancel;

        public MaintenanceRecord NewRecord { get; set; }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public NewCarMaintenanceViewModel(Action<MaintenanceRecord> record, Action cancel)
        {
            this.record = record;
            this.cancel = cancel;

            NewRecord = new MaintenanceRecord();

            SaveCommand = new RelayCommand(() => record(NewRecord));
            CancelCommand = new RelayCommand(cancel);
        }


    }
}
